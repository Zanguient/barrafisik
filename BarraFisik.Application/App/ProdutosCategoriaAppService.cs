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
    public class ProdutosCategoriaAppService : AppServiceBase<BarraFisikContext>, IProdutosCategoriaAppService
    {
        private readonly IProdutosCategoriaService _produtosCategoriaService;
        private readonly IProdutosService _produtosService;
        private readonly ILogSistemaService _logSistemaService;

        public ProdutosCategoriaAppService(IProdutosCategoriaService produtosCategoriaService, ILogSistemaService logSistemaService, IProdutosService produtosService)
        {
            _produtosCategoriaService = produtosCategoriaService;
            _logSistemaService = logSistemaService;
            _produtosService = produtosService;
        }

        public void Add(ProdutosCategoriaViewModel produtosCategoriaViewModel)
        {
            var produtoCategoria = Mapper.Map<ProdutosCategoriaViewModel, ProdutosCategoria>(produtosCategoriaViewModel);

            BeginTransaction();
            _produtosCategoriaService.Add(produtoCategoria);

            _logSistemaService.AddLog("Produtos Categoria", produtosCategoriaViewModel.ProdutoCategoriaId, "Cadastro", "Descrição: " + produtosCategoriaViewModel.Nome);
            Commit();
        }        

        public IEnumerable<ProdutosCategoriaViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<ProdutosCategoria>, IEnumerable<ProdutosCategoriaViewModel>>(_produtosCategoriaService.GetAll());
        }

        public ProdutosCategoriaViewModel GetById(Guid id)
        {
            return Mapper.Map<ProdutosCategoria, ProdutosCategoriaViewModel>(_produtosCategoriaService.GetById(id));
        }

        public void Update(ProdutosCategoriaViewModel produtosCategoriaViewModel)
        {
            var produtoCategoria = Mapper.Map<ProdutosCategoriaViewModel, ProdutosCategoria>(produtosCategoriaViewModel);

            BeginTransaction();
            _produtosCategoriaService.Update(produtoCategoria);

            _logSistemaService.AddLog("Produtos Categoria", produtosCategoriaViewModel.ProdutoCategoriaId, "Update", "Descrição: " + produtosCategoriaViewModel.Nome);
            Commit();
        }

        public void Remove(Guid id)
        {
            var produto = Mapper.Map<ProdutosCategoriaViewModel, ProdutosCategoria>(GetById(id));

            BeginTransaction();
            _produtosCategoriaService.Remove(produto);

            //Remove todos os produtos
            foreach (var item in _produtosService.GetByCategoria(produto.ProdutoCategoriaId))
            {
                _produtosService.Remove(item);
            }

            _logSistemaService.AddLog("Produtos Categoria", produto.ProdutoCategoriaId, "Remove", "Descrição: " + produto.Nome);
            Commit();
        }

        public void Dispose()
        {
            _produtosCategoriaService.Dispose();
        }
    }
}