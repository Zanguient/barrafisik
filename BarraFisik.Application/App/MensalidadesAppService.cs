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
    public class MensalidadesAppService : AppServiceBase<BarraFisikContext>, IMensalidadesAppService
    {
        private readonly IMensalidadesService _mensalidadesService;
        private readonly IClienteService _clienteService;

        public MensalidadesAppService(IMensalidadesService mensalidadesService, IClienteService clienteService)
        {
            _mensalidadesService = mensalidadesService;
            _clienteService = clienteService;
        }


        public void Add(MensalidadesViewModel mensalidadesViewModel)
        {
            var mensalidade = Mapper.Map<MensalidadesViewModel, Mensalidades>(mensalidadesViewModel);

            BeginTransaction();
            _mensalidadesService.Add(mensalidade);
            Commit();
        }

        public ValidationAppResult AdicionarMensalidade(MensalidadesViewModel mensalidadesViewModel)
        {
            var mensalidade = Mapper.Map<MensalidadesViewModel, Mensalidades>(mensalidadesViewModel);

            BeginTransaction();

            var result = _mensalidadesService.AdicionarMensalidade(mensalidade);

            if (!result.IsValid)
                return DomainToApplicationResult(result);

            //Atualiza situacao do cliente caso a mensalidade que esta sendo paga se refere ao mês atual
            var today = DateTime.Now;
            if (mensalidadesViewModel.MesReferencia >= today.Month && mensalidadesViewModel.AnoReferencia >= today.Year)
            {
                var cliente = _clienteService.GetById(mensalidadesViewModel.ClienteId);
                cliente.Situacao = "Regular";
                _clienteService.Update(cliente);
            }

            Commit();

            return DomainToApplicationResult(result);
        }

        public MensalidadesViewModel GetById(Guid id)
        {
            return Mapper.Map<Mensalidades, MensalidadesViewModel>(_mensalidadesService.GetById(id));
        }

        public IEnumerable<MensalidadesViewModel> GetAll()
        {
            return
                Mapper.Map<IEnumerable<Mensalidades>, IEnumerable<MensalidadesViewModel>>(_mensalidadesService.GetAll());
        }

        public IEnumerable<MensalidadesViewModel> GetMensalidadesCliente(Guid id)
        {
            return
                Mapper.Map<IEnumerable<Mensalidades>, IEnumerable<MensalidadesViewModel>>(
                    _mensalidadesService.GetMensalidadesCliente(id));
        }

        public ValidationAppResult Update(MensalidadesViewModel mensalidadesViewModel)
        {
            var mensalidade = Mapper.Map<MensalidadesViewModel, Mensalidades>(mensalidadesViewModel);

            BeginTransaction();

            var result = _mensalidadesService.AdicionarMensalidade(mensalidade);

            if (!result.IsValid)
                return DomainToApplicationResult(result);            

            Commit();


            BeginTransaction();
            //Verifica se existe alguma mensalidade com mes e ano maior ou igual a data atual            
            var today = DateTime.Now;
            bool existeMensalidade = false;
            var cliente = _clienteService.GetById(mensalidadesViewModel.ClienteId);
            foreach (var mensalidades in GetMensalidadesCliente(mensalidadesViewModel.ClienteId))
            {
                if (mensalidades.MesReferencia >= today.Month && mensalidades.AnoReferencia >= today.Year)
                {
                    existeMensalidade = true;
                }
            }

            if (existeMensalidade && cliente.Situacao != "Regular")
            {
                cliente.Situacao = "Regular";
                _clienteService.Update(cliente);
            }
            else
            {
                cliente.Situacao = "Pendente";
                _clienteService.Update(cliente);
            }

            Commit();

            return DomainToApplicationResult(result);
        }

        public void Remove(Guid id)
        {
            var mensalidade = Mapper.Map<MensalidadesViewModel, Mensalidades>(GetById(id));

            BeginTransaction();
            _mensalidadesService.Remove(mensalidade);
            Commit();

            BeginTransaction();
            //Verifica se existe alguma mensalidade com mes e ano maior ou igual a data atual            
            var today = DateTime.Now;
            bool existeMensalidade = false;
            var cliente = _clienteService.GetById(mensalidade.ClienteId);
            foreach (var mensalidades in GetMensalidadesCliente(mensalidade.ClienteId))
            {
                if (mensalidades.MesReferencia >= today.Month && mensalidades.AnoReferencia >= today.Year)
                {
                    existeMensalidade = true;
                }
            }

            if (existeMensalidade && cliente.Situacao != "Regular")
            {
                cliente.Situacao = "Regular";
                _clienteService.Update(cliente);
            }
            else
            {
                cliente.Situacao = "Pendente";
                _clienteService.Update(cliente);
            }
            Commit();
        }

        public void Dispose()
        {
            _mensalidadesService.Dispose();
        }

    }
}