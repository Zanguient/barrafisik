using System;
using System.Collections.Generic;
using AutoMapper;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.Validation;
using BarraFisik.Application.ViewModels;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Application.App
{
    public class ClienteAppService : AppServiceBase<BarraFisikContext>, IClienteAppService
    {
        private readonly IClienteService _clienteService;

        public ClienteAppService(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }


        public ValidationAppResult Add(ClienteViewModel clienteViewModel)
        {
            var cliente = Mapper.Map<ClienteViewModel, Cliente>(clienteViewModel);

            BeginTransaction();

            var result = _clienteService.AdicionarCliente(cliente);

            if (!result.IsValid)
                return DomainToApplicationResult(result);

            Commit();

            return DomainToApplicationResult(result);
        }

        public ClienteViewModel GetById(Guid id)
        {
            return Mapper.Map<Cliente, ClienteViewModel>(_clienteService.GetById(id));
        }

        public IEnumerable<ClienteViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(_clienteService.GetClientes());
        }

        public ValidationAppResult Update(ClienteViewModel clienteViewModel)
        {
            var cliente = Mapper.Map<ClienteViewModel, Cliente>(clienteViewModel);

            var result = _clienteService.AtualizarCliente(cliente);

            BeginTransaction();

            if (!result.IsValid)
                return DomainToApplicationResult(result);

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

        public ClienteViewModel GetClienteHorario(Guid id)
        {
            return Mapper.Map<Cliente, ClienteViewModel>(_clienteService.GetById(id));
        }

        public void Dispose()
        {
            _clienteService.Dispose();
        }
    }
}