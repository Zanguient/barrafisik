using System.Collections.Generic;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Infra.Data.Context;
using AutoMapper;
using BarraFisik.Domain.Entities;
using System;

namespace BarraFisik.Application.App
{
    public class ArmazemAppService : AppServiceBase<BarraFisikContext>, IArmazemAppService
    {
        private readonly IArmazemService _armazemService;
        private readonly ILogSistemaService _logSistemaService;

        public ArmazemAppService(IArmazemService armazemService, ILogSistemaService logSistemaService)
        {
            _armazemService = armazemService;
            _logSistemaService = logSistemaService;
        }

        public void Add(ArmazemViewModel armazemViewModel)
        {
            var armazem = Mapper.Map<ArmazemViewModel, Armazem>(armazemViewModel);

            BeginTransaction();
            _armazemService.Add(armazem);

            _logSistemaService.AddLog("Armazem", armazemViewModel.ArmazemId, "Cadastro", "Descrição: "+armazemViewModel.Descricao);
            Commit();
        }

        public ArmazemViewModel GetById(Guid id)
        {
            return Mapper.Map<Armazem, ArmazemViewModel>(_armazemService.GetById(id));
        }

        public IEnumerable<ArmazemViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Armazem>, IEnumerable<ArmazemViewModel>>(_armazemService.GetAll());
        }

        public void Update(ArmazemViewModel armazemViewModel)
        {
            var armazem = Mapper.Map<ArmazemViewModel, Armazem>(armazemViewModel);

            BeginTransaction();
            _armazemService.Update(armazem);

            _logSistemaService.AddLog("Armazem", armazemViewModel.ArmazemId, "Update", "Descrição: " + armazemViewModel.Descricao);
            Commit();
        }

        public void Remove(Guid id)
        {
            var armazem = Mapper.Map<ArmazemViewModel, Armazem>(GetById(id));

            BeginTransaction();
            _armazemService.Remove(armazem);

            _logSistemaService.AddLog("Armazem", armazem.ArmazemId, "Remove", "Descrição: " + armazem.Descricao);
            Commit();
        }

        public void Dispose()
        {
            _armazemService.Dispose();
        }
    }
}
