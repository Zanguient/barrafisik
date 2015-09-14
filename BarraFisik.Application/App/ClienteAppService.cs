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

        public ClienteAppService(IClienteService clienteService, IHorarioService horarioService)
        {
            _clienteService = clienteService;
            _horarioService = horarioService;
        }


        public ValidationAppResult Add(ClienteHorarioViewModel clienteHorarioViewModel)
        {
            var cliente = Mapper.Map<ClienteHorarioViewModel, Cliente>(clienteHorarioViewModel);
            var horario = Mapper.Map<ClienteHorarioViewModel, Horario>(clienteHorarioViewModel);

            BeginTransaction();

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


        public IEnumerable<ClienteViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(_clienteService.GetClientes());
        }

        public ValidationAppResult Update(ClienteHorarioViewModel clienteHorarioViewModel)
        {
            var cliente = Mapper.Map<ClienteHorarioViewModel, Cliente>(clienteHorarioViewModel);
            var horario = Mapper.Map<ClienteHorarioViewModel, Horario>(clienteHorarioViewModel);

            var hasHorario = _horarioService.GetHorarioCliente(cliente.ClienteId);

            BeginTransaction();

            var c = _clienteService.GetById(cliente.ClienteId);
            c = cliente;

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
                _horarioService.Update(h);
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

        public void Dispose()
        {
            _clienteService.Dispose();
        }
    }
}