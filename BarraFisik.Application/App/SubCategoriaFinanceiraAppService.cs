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
    public class SubCategoriaFinanceiraAppService : AppServiceBase<BarraFisikContext>, ISubCategoriaFinanceiraAppService
    {
        private readonly ISubCategoriaFinanceiraService _subCategoriaFinanceiraService;
        private readonly ILogSistemaService _logSistemaService;

        public SubCategoriaFinanceiraAppService(ISubCategoriaFinanceiraService subCategoriaFinanceiraService, ILogSistemaService logSistemaService)
        {
            _subCategoriaFinanceiraService = subCategoriaFinanceiraService;
            _logSistemaService = logSistemaService;
        }


        public void Add(SubCategoriaFinanceiraViewModel subCategoriaFinanceiraViewModel)
        {
            var subCategoria =  Mapper.Map<SubCategoriaFinanceiraViewModel, SubCategoriaFinanceira>(subCategoriaFinanceiraViewModel);

            BeginTransaction();
            _subCategoriaFinanceiraService.Add(subCategoria);

            _logSistemaService.AddLog("SubCategoriaFinanceira", subCategoriaFinanceiraViewModel.SubCategoriaFinanceiraId, "Cadastro", "");
            Commit();
        }

        public SubCategoriaFinanceiraViewModel GetById(Guid id)
        {
            return Mapper.Map<SubCategoriaFinanceira, SubCategoriaFinanceiraViewModel>(_subCategoriaFinanceiraService.GetById(id));
        }

        public IEnumerable<SubCategoriaFinanceiraViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<SubCategoriaFinanceira>, IEnumerable<SubCategoriaFinanceiraViewModel>>(_subCategoriaFinanceiraService.GetAll());
        }

        public IEnumerable<SubCategoriaFinanceiraViewModel> GetByCategoria(Guid idCategoria)
        {
            return Mapper.Map<IEnumerable<SubCategoriaFinanceira>, IEnumerable<SubCategoriaFinanceiraViewModel>>(_subCategoriaFinanceiraService.GetByCategoria(idCategoria));
        }

        public void Update(SubCategoriaFinanceiraViewModel subCategoriaFinanceiraViewModel)
        {
            var subCategoria = Mapper.Map<SubCategoriaFinanceiraViewModel, SubCategoriaFinanceira>(subCategoriaFinanceiraViewModel);

            BeginTransaction();
            _subCategoriaFinanceiraService.Update(subCategoria);

            _logSistemaService.AddLog("CategoriaFinanceira", subCategoriaFinanceiraViewModel.SubCategoriaFinanceiraId, "Update", "SubCategoria: " + subCategoria.SubCategoria);
            Commit();
        }

        public void Remove(Guid id)
        {
            var subCategoria = Mapper.Map<SubCategoriaFinanceiraViewModel, SubCategoriaFinanceira>(GetById(id));

            BeginTransaction();
            _subCategoriaFinanceiraService.Remove(subCategoria);

            _logSistemaService.AddLog("SubCategoriaFinanceira", id, "Delete", "SubCategoria: "+ subCategoria.SubCategoria);
            Commit();
        }

        public void Dispose()
        {
            _subCategoriaFinanceiraService.Dispose();
        }

        
    }
}