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
    public class ProdutosAppService : AppServiceBase<BarraFisikContext>, IProdutosAppService
    {
        private readonly IProdutosService _produtosService;
        private readonly ILogSistemaService _logSistemaService;

        public ProdutosAppService(IProdutosService produtosService, ILogSistemaService logSistemaService)
        {
            _produtosService = produtosService;
            _logSistemaService = logSistemaService;
        }

        public void Add(ProdutosViewModel produtosViewModel)
        {
            var produto = Mapper.Map<ProdutosViewModel, Produtos>(produtosViewModel);

            BeginTransaction();
            _produtosService.Add(produto);


            _logSistemaService.AddLog("Produtos", produtosViewModel.ProdutoId, "Cadastro", "Descrição: " + produtosViewModel.Nome);
            Commit();
        }        

        public IEnumerable<ProdutosViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Produtos>, IEnumerable<ProdutosViewModel>>(_produtosService.GetAll());
        }

        public IEnumerable<ProdutosViewModel> GetProdutos()
        {
            return Mapper.Map<IEnumerable<Produtos>, IEnumerable<ProdutosViewModel>>(_produtosService.GetProdutos());
        }

        public ProdutosViewModel GetById(Guid id)
        {
            return Mapper.Map<Produtos, ProdutosViewModel>(_produtosService.GetById(id));
        }

        public IEnumerable<ProdutosViewModel> GetByCategoria(Guid id)
        {
            return Mapper.Map<IEnumerable<Produtos>, IEnumerable<ProdutosViewModel>>(_produtosService.GetByCategoria(id));
        }

        public void Update(ProdutosViewModel produtosViewModel)
        {
            var produto = Mapper.Map<ProdutosViewModel, Produtos>(produtosViewModel);

            BeginTransaction();
            _produtosService.Add(produto);

            _logSistemaService.AddLog("Produtos", produtosViewModel.ProdutoId, "Update", "Descrição: " + produtosViewModel.Nome);
            Commit();
        }

        public void Remove(Guid id)
        {
            var produto = Mapper.Map<ProdutosViewModel, Produtos>(GetById(id));

            BeginTransaction();
            _produtosService.Remove(produto);

            _logSistemaService.AddLog("Produtos", produto.ProdutoId, "Remove", "Descrição: " + produto.Nome);
            Commit();
        }

        public void Dispose()
        {
            _produtosService.Dispose();
        }
    }
}