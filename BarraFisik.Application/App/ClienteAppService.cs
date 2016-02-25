using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.Validation;
using BarraFisik.Application.ViewModels;
using BarraFisik.API.Models;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Domain.ValueObjects;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Application.App
{
    public class ClienteAppService : AppServiceBase<BarraFisikContext>, IClienteAppService
    {
        private readonly IClienteService _clienteService;
        private readonly IHorarioService _horarioService;
        private readonly IMensalidadesService _mensalidadesService;
        private readonly IValoresService _valoresService;
        private readonly ILogSistemaService _logSistemaService;

        public ClienteAppService(IClienteService clienteService, IHorarioService horarioService, IMensalidadesService mensalidadesService, IValoresService valoresService, ILogSistemaService logSistemaService)
        {
            _clienteService = clienteService;
            _horarioService = horarioService;
            _mensalidadesService = mensalidadesService;
            _valoresService = valoresService;
            _logSistemaService = logSistemaService;
        }

        public ValidationAppResult Add(ClienteHorarioViewModel clienteHorarioViewModel)
        {
            var cliente = Mapper.Map<ClienteHorarioViewModel, Cliente>(clienteHorarioViewModel);
            var horario = Mapper.Map<ClienteHorarioViewModel, Horario>(clienteHorarioViewModel);

            var valor = GetValor(horario);

            if (cliente.Cpf == "")
                cliente.Cpf = null;

            if (valor != null)
                cliente.ValoresId = valor.ValoresId;

            cliente.Situacao = !cliente.IsAtivo ? "Inativo" : "Pendente";

            BeginTransaction();
            var result = _clienteService.AdicionarCliente(cliente);

            if (!result.IsValid)
                return DomainToApplicationResult(result);

            //Cadastra Horario                      
            _horarioService.Add(horario);

            _logSistemaService.AddLog("Cliente", cliente.ClienteId, "Cadastro", "");
            Commit();

            return DomainToApplicationResult(result);
        }

        public void AtualizaValores()
        {
            BeginTransaction();
            foreach (var cliente in _clienteService.GetClientes())
            {
                var horario = _horarioService.GetHorarioCliente(cliente.ClienteId);
                var valor = GetValor(horario);
                if (valor != null)
                {
                    _clienteService.AtualizaValores(cliente.ClienteId, valor.ValoresId);
                }
                
            }
            Commit();
        }

        public ClienteViewModel GetById(Guid id)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ClienteHorarioViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<ClienteHorario>, IEnumerable<ClienteHorarioViewModel>>(_clienteService.GetClientes());
        }

        public IEnumerable<ClienteHorarioViewModel> GetClientesAll()
        {
            return Mapper.Map<IEnumerable<ClienteHorario>, IEnumerable<ClienteHorarioViewModel>>(_clienteService.GetClientesAll());
        }

        public ValidationAppResult Update(ClienteHorarioViewModel clienteHorarioViewModel)
        {
            var cliente = Mapper.Map<ClienteHorarioViewModel, Cliente>(clienteHorarioViewModel);
            var horario = Mapper.Map<ClienteHorarioViewModel, Horario>(clienteHorarioViewModel);

            var hasHorario = _horarioService.GetHorarioCliente(cliente.ClienteId);

            var valor = GetValor(horario);

            BeginTransaction();
           
            var c = _clienteService.GetById(cliente.ClienteId);
            c = cliente;

            if (!c.IsAtivo)
                c.Situacao = "Inativo";
            else if(c.IsAtivo && c.Situacao == "Inativo" || c.Situacao == null)
            {
                //Caso esteja ativando novamente o cliente, verifica se o mesmo já possui mensalidade paga atual            
                var today = DateTime.Now;
                bool existeMensalidade = false;
                foreach (var mensalidades in _mensalidadesService.GetMensalidadesCliente(c.ClienteId))
                {
                    if (mensalidades.MesReferencia >= today.Month && mensalidades.AnoReferencia >= today.Year)
                    {
                        existeMensalidade = true;
                    }
                }

                if (existeMensalidade && cliente.Situacao != "Regular")
                {
                    c.Situacao = "Regular";
                    _clienteService.Update(cliente);
                }
                else
                {
                    c.Situacao = "Pendente";
                    _clienteService.Update(cliente);
                }

            }
            if(c.IsAtivo && valor != null)
                c.ValoresId = valor.ValoresId;
            var result = _clienteService.AtualizarCliente(c);

            if (!result.IsValid)
                return DomainToApplicationResult(result);

            var h = _horarioService.GetById(horario.HorarioId);
            h = horario;
            ////Verifica se cliente não está ativo, e existe horario cadastrado para o mesmo, se existir remove o horario.
            if (!cliente.IsAtivo && hasHorario != null)
            {
                _horarioService.Remove(h);
            }
            else
            {
                //Adiciona ou Atualiza
                _horarioService.Add(h);
            }

            _logSistemaService.AddLog("Cliente", cliente.ClienteId, "Update", "");
            Commit();

            return DomainToApplicationResult(result);
        }

        public void Remove(ClienteViewModel clienteViewModel)
        {
            var cliente = Mapper.Map<ClienteViewModel, Cliente>(clienteViewModel);

            BeginTransaction();
            _clienteService.Remove(cliente);

            _logSistemaService.AddLog("Cliente", cliente.ClienteId, "Remove", "Cliente"+cliente.Nome);
            Commit();
        }

        public ClienteHorarioViewModel GetByClienteId(Guid id)
        {
            return Mapper.Map<ClienteHorario, ClienteHorarioViewModel>(_clienteService.GetByClienteId(id));
        }

        public ClienteViewModel GetClienteHorario(Guid id)
        {
            return Mapper.Map<Cliente, ClienteViewModel>(_clienteService.GetById(id));
        }

        public IEnumerable<ClienteViewModel> GetAniversariantes(int mes)
        {
            return Mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(_clienteService.GetAniversariantes(mes));
        }

        public void UpdateClientesPendentes(int mes, int ano)
        {
            _clienteService.UpdateClientesPendentes(mes, ano);
        }

        public IEnumerable<ClienteViewModel> GetClientesSituacao(string situacao)
        {
            return Mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(_clienteService.GetClientesSituacao(situacao));
        }

        public void InativarClientes(IEnumerable<ClienteViewModel> listClientes)
        {
            var clientesLista = Mapper.Map<IEnumerable<ClienteViewModel>, IEnumerable<Cliente>>(listClientes);

            BeginTransaction();
            _clienteService.InativarClientes(clientesLista);

            foreach (var cliente in clientesLista)
            {
                _logSistemaService.AddLog("Cliente", cliente.ClienteId, "Inativado", "Cliente: " + cliente.ClienteId);
            }
            Commit();
        }

        public TotalInscritosViewModel GetTotalInscritos(int ano)
        {
            return Mapper.Map<TotalInscritos, TotalInscritosViewModel>(_clienteService.GetTotalInscritos(ano));
        }

        public ClienteHorarioViewModel GetClientePerfil(Guid id)
        {
            return Mapper.Map<ClienteHorario, ClienteHorarioViewModel>(_clienteService.GetClientePerfil(id));
        }

        private Valores GetValor(Horario horario)
        {
            //Pega qtd de dias, e maior horario
            int qtdDias = 0;
            int maiorHorario = 0;
            if (horario.Segunda) qtdDias = qtdDias + 1;
            if (horario.Terca) qtdDias = qtdDias + 1;
            if (horario.Quarta) qtdDias = qtdDias + 1;
            if (horario.Quinta) qtdDias = qtdDias + 1;
            if (horario.Sexta) qtdDias = qtdDias + 1;

            if (horario.HSegunda != null && Int32.Parse(horario.HSegunda) > maiorHorario) maiorHorario = Int32.Parse(horario.HSegunda);
            if (horario.HTerca != null && Int32.Parse(horario.HTerca) > maiorHorario) maiorHorario = Int32.Parse(horario.HTerca);
            if (horario.HQuarta != null && Int32.Parse(horario.HQuarta) > maiorHorario) maiorHorario = Int32.Parse(horario.HQuarta);
            if (horario.HQuinta != null && Int32.Parse(horario.HQuinta) > maiorHorario) maiorHorario = Int32.Parse(horario.HQuinta);
            if (horario.HSexta != null && Int32.Parse(horario.HSexta) > maiorHorario) maiorHorario = Int32.Parse(horario.HSexta);

            var valor = _valoresService.GetValorCliente(qtdDias, maiorHorario);
            return valor;
        }

        public void Dispose()
        {
            _clienteService.Dispose();
        }

        
    }
}