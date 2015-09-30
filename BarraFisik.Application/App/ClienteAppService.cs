using System;
using System.Collections.Generic;
using AutoMapper;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.Validation;
using BarraFisik.Application.ViewModels;
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

        public ClienteAppService(IClienteService clienteService, IHorarioService horarioService, IMensalidadesService mensalidadesService)
        {
            _clienteService = clienteService;
            _horarioService = horarioService;
            _mensalidadesService = mensalidadesService;
        }


        public ValidationAppResult Add(ClienteHorarioViewModel clienteHorarioViewModel)
        {
            var cliente = Mapper.Map<ClienteHorarioViewModel, Cliente>(clienteHorarioViewModel);
            var horario = Mapper.Map<ClienteHorarioViewModel, Horario>(clienteHorarioViewModel);

            BeginTransaction();

            if (!cliente.IsAtivo)
                cliente.Situacao = "Inativo";
            else cliente.Situacao = "Pendente";
            var result = _clienteService.AdicionarCliente(cliente);

            if (!result.IsValid)
                return DomainToApplicationResult(result);

            //Cadastra Horario
            _horarioService.Add(horario);

            Commit();

            return DomainToApplicationResult(result);
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

            Commit();

            return DomainToApplicationResult(result);
        }

        public void Remove(ClienteViewModel clienteViewModel)
        {
            var cliente = Mapper.Map<ClienteViewModel, Cliente>(clienteViewModel);

            BeginTransaction();
            _clienteService.Remove(cliente);
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
            Commit();
        }

        public TotalInscritosViewModel GetTotalInscritos(int ano)
        {
            return Mapper.Map<TotalInscritos, TotalInscritosViewModel>(_clienteService.GetTotalInscritos(ano));
        }

        public void Dispose()
        {
            _clienteService.Dispose();
        }
    }
}