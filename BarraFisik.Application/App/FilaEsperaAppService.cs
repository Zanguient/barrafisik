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
    public class FilaEsperaAppService : AppServiceBase<BarraFisikContext>, IFilaEsperaAppService
    {
        private readonly IFilaEsperaService _filaEsperaService;
        private readonly ILogSistemaService _logSistemaService;

        public FilaEsperaAppService(IFilaEsperaService filaEsperaService, ILogSistemaService logSistemaService)
        {
            _filaEsperaService = filaEsperaService;
            _logSistemaService = logSistemaService;
        }

        public void Add(FilaEsperaViewModel filaEsperaViewModel)
        {
            var filaEspera =  Mapper.Map<FilaEsperaViewModel, FilaEspera>(filaEsperaViewModel);

            BeginTransaction();
            _filaEsperaService.Add(filaEspera);

            _logSistemaService.AddLog("FilaDeEspera", filaEsperaViewModel.FilaEsperaId, "Cadastro", "");
            Commit();
        }

        public FilaEsperaViewModel GetById(Guid id)
        {
            return Mapper.Map<FilaEspera, FilaEsperaViewModel>(_filaEsperaService.GetById(id));
        }

        public IEnumerable<FilaEsperaViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<FilaEspera>, IEnumerable<FilaEsperaViewModel>>(_filaEsperaService.GetAll());
        }

        public void Update(FilaEsperaViewModel filaEsperaViewModel)
        {
            var filaEspera = Mapper.Map<FilaEsperaViewModel, FilaEspera>(filaEsperaViewModel);

            BeginTransaction();
            _filaEsperaService.Update(filaEspera);

            _logSistemaService.AddLog("FilaDeEspera", filaEsperaViewModel.FilaEsperaId, "Update", "");
            Commit();
        }

        public void Remove(Guid id)
        {
            var filaEspera = Mapper.Map<FilaEsperaViewModel, FilaEspera>(GetById(id));

            BeginTransaction();
            _filaEsperaService.Remove(filaEspera);

            _logSistemaService.AddLog("FilaDeEspera", id, "Delete", "Cliente: "+filaEspera.Nome);
            Commit();
        }

        public void Dispose()
        {
            _filaEsperaService.Dispose();
        }
    }
}