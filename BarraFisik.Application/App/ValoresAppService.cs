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
        private readonly ILogSistemaService _logSistemaService;

        public ValoresAppService(IValoresService valoresService, ILogSistemaService logSistemaService)
        {
            _valoresService = valoresService;
            _logSistemaService = logSistemaService;
        }

        public void Add(ValoresViewModel valoresViewModel)
        {
            var valores =  Mapper.Map<ValoresViewModel, Valores>(valoresViewModel);

            BeginTransaction();
            _valoresService.Add(valores);

            _logSistemaService.AddLog("Valores", valoresViewModel.ValoresId, "Cadastro", "QtdDias:"+valores.QtdDias+" - "+valores.Valor);
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

        public void Update(ValoresViewModel valoresViewModel)
        {
            var valores = Mapper.Map<ValoresViewModel, Valores>(valoresViewModel);

            BeginTransaction();
            _valoresService.Update(valores);

            _logSistemaService.AddLog("Valores", valoresViewModel.ValoresId, "Update", "QtdDias:" + valores.QtdDias + " - " + valores.Valor);
            Commit();
        }

        public void Remove(Guid id)
        {
            var valores = Mapper.Map<ValoresViewModel, Valores>(GetById(id));

            BeginTransaction();
            _valoresService.Remove(valores);

            _logSistemaService.AddLog("Valores", id, "Cadastro", "QtdDias:" + valores.QtdDias + " - " + valores.Valor);
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