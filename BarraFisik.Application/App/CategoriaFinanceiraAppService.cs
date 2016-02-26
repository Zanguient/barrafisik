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
    public class CategoriaFinanceiraAppService : AppServiceBase<BarraFisikContext>, ICategoriaFinanceiraAppService
    {
        private readonly ICategoriaFinanceiraService _categoriaFinanceiraService;
        private readonly ISubCategoriaFinanceiraService _subCategoriaFinanceiraService;
        private readonly ILogSistemaService _logSistemaService;

        public CategoriaFinanceiraAppService(ICategoriaFinanceiraService categoriaFinanceiraService, ILogSistemaService logSistemaService, ISubCategoriaFinanceiraService subCategoriaFinanceiraService)
        {
            _categoriaFinanceiraService = categoriaFinanceiraService;
            _logSistemaService = logSistemaService;
            _subCategoriaFinanceiraService = subCategoriaFinanceiraService;
        }


        public void Add(CategoriaFinanceiraViewModel categoriaFinanceiraViewModel)
        {
            var categoria =  Mapper.Map<CategoriaFinanceiraViewModel, CategoriaFinanceira>(categoriaFinanceiraViewModel);

            BeginTransaction();
            _categoriaFinanceiraService.Add(categoria);

            _logSistemaService.AddLog("CategoriaFinanceira", categoriaFinanceiraViewModel.CategoriaFinanceiraId, "Cadastro", "");
            Commit();
        }

        public CategoriaFinanceiraViewModel GetById(Guid id)
        {
            return Mapper.Map<CategoriaFinanceira, CategoriaFinanceiraViewModel>(_categoriaFinanceiraService.GetById(id));
        }

        public IEnumerable<CategoriaFinanceiraViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<CategoriaFinanceira>, IEnumerable<CategoriaFinanceiraViewModel>>(_categoriaFinanceiraService.GetAll());
        }

        public IEnumerable<CategoriaFinanceiraViewModel> GetByTipo(string tipo)
        {
            return Mapper.Map<IEnumerable<CategoriaFinanceira>, IEnumerable<CategoriaFinanceiraViewModel>>(_categoriaFinanceiraService.GetByTipo(tipo));
        }

        public void Update(CategoriaFinanceiraViewModel categoriaFinanceiraViewModel)
        {
            var categoria = Mapper.Map<CategoriaFinanceiraViewModel, CategoriaFinanceira>(categoriaFinanceiraViewModel);

            BeginTransaction();
            _categoriaFinanceiraService.Update(categoria);

            _logSistemaService.AddLog("CategoriaFinanceira", categoriaFinanceiraViewModel.CategoriaFinanceiraId, "Update", "");
            Commit();
        }

        public void Remove(Guid id)
        {
            var categoria = Mapper.Map<CategoriaFinanceiraViewModel, CategoriaFinanceira>(GetById(id));

            BeginTransaction();
            _categoriaFinanceiraService.Remove(categoria);

            //Remove todas subCategorias
            foreach (var item in _subCategoriaFinanceiraService.GetByCategoria(categoria.CategoriaFinanceiraId))
            {
                _subCategoriaFinanceiraService.Remove(item);
            }

            _logSistemaService.AddLog("CategoriaFinanceira", id, "Delete", "Categoria: "+categoria.Categoria);
            Commit();
        }

        public void Dispose()
        {
            _categoriaFinanceiraService.Dispose();
        }
    }
}