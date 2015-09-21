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

        public FilaEsperaAppService(IFilaEsperaService filaEsperaService)
        {
            _filaEsperaService = filaEsperaService;
        }

        public void Add(FilaEsperaViewModel filaEsperaViewModel)
        {
            var filaEspera =  Mapper.Map<FilaEsperaViewModel, FilaEspera>(filaEsperaViewModel);

            BeginTransaction();
            _filaEsperaService.Add(filaEspera);
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
            Commit();
        }

        public void Remove(Guid id)
        {
            var filaEspera = Mapper.Map<FilaEsperaViewModel, FilaEspera>(GetById(id));

            BeginTransaction();
            _filaEsperaService.Remove(filaEspera);
            Commit();
        }

        public void Dispose()
        {
            _filaEsperaService.Dispose();
        }
    }
}