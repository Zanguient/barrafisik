using System;
using System.Collections.Generic;
using AutoMapper;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Application.App
{
    public class ValoresAppService : AppServiceBase<BarraFisikContext>, IValoresAppService
    {
        private readonly IValoresService _valoresService;

        public ValoresAppService(IValoresService valoresService)
        {
            _valoresService = valoresService;
        }

        public void Add(ValoresViewModel valoresViewModel)
        {
            var valores =  Mapper.Map<ValoresViewModel, Valores>(valoresViewModel);

            BeginTransaction();
            _valoresService.Add(valores);
            Commit();
        }

        public ValoresViewModel GetById(Guid id)
        {
            return Mapper.Map<Valores, ValoresViewModel>(_valoresService.GetById(id));
        }

        public IEnumerable<ValoresViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Valores>, IEnumerable<ValoresViewModel>>(_valoresService.GetAll());
        }

        public void Update(ValoresViewModel filaEsperaViewModel)
        {
            var valores = Mapper.Map<ValoresViewModel, Valores>(filaEsperaViewModel);

            BeginTransaction();
            _valoresService.Update(valores);
            Commit();
        }

        public void Remove(Guid id)
        {
            var valores = Mapper.Map<ValoresViewModel, Valores>(GetById(id));

            BeginTransaction();
            _valoresService.Remove(valores);
            Commit();
        }

        public ValoresViewModel GetValorCliente(int qtdDias, int horario)
        {            
           return Mapper.Map<Valores, ValoresViewModel>(_valoresService.GetValorCliente(qtdDias, horario));
        }

        public void Dispose()
        {
            _valoresService.Dispose();
        }
    }
}