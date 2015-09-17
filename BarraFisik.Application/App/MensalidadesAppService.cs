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
    public class MensalidadesAppService : AppServiceBase<BarraFisikContext>, IMensalidadesAppService
    {
        private readonly IMensalidadesService _mensalidadesService;

        public MensalidadesAppService(IMensalidadesService mensalidadesService)
        {
            _mensalidadesService = mensalidadesService;
        }


        public void Add(MensalidadesViewModel mensalidadesViewModel)
        {
            var mensalidade =  Mapper.Map<MensalidadesViewModel, Mensalidades>(mensalidadesViewModel);

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

            Commit();

            return DomainToApplicationResult(result);
        }

        public MensalidadesViewModel GetById(Guid id)
        {
            return Mapper.Map<Mensalidades, MensalidadesViewModel>(_mensalidadesService.GetById(id));
        }

        public IEnumerable<MensalidadesViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Mensalidades>, IEnumerable<MensalidadesViewModel>>(_mensalidadesService.GetAll());
        }

        public IEnumerable<MensalidadesViewModel> GetMensalidadesCliente(Guid id)
        {
            return Mapper.Map<IEnumerable<Mensalidades>, IEnumerable<MensalidadesViewModel>>(_mensalidadesService.GetMensalidadesCliente(id));
        }

        public void Update(MensalidadesViewModel mensalidadesViewModel)
        {
            var mensalidade = Mapper.Map<MensalidadesViewModel, Mensalidades>(mensalidadesViewModel);

            BeginTransaction();
            _mensalidadesService.Update(mensalidade);
            Commit();
        }

        public void Remove(Guid id)
        {
            var mensalidade = Mapper.Map<MensalidadesViewModel, Mensalidades>(GetById(id));

            BeginTransaction();
            _mensalidadesService.Remove(mensalidade);
            Commit();
        }

        public void Dispose()
        {
            _mensalidadesService.Dispose();
        }
    }
}