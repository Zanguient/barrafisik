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
    public class VendasProdutosAppService : AppServiceBase<BarraFisikContext>, IVendasProdutosAppService
    {
        private readonly IVendasProdutosService _vendasProdutosService;
        private readonly IEstoqueService _estoqueService;
        private readonly IVendasService _vendasService;
        private readonly IReceitasService _receitasService;
        private readonly ILogSistemaService _logSistemaService;

        public VendasProdutosAppService(IVendasProdutosService vendasProdutosService, ILogSistemaService logSistemaService, IEstoqueService estoqueService, IVendasService vendasService, IReceitasService receitasService)
        {
            _vendasProdutosService = vendasProdutosService;
            _logSistemaService = logSistemaService;
            _estoqueService = estoqueService;
            _vendasService = vendasService;
            _receitasService = receitasService;
        }

        public IEnumerable<VendasProdutosViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<VendasProdutos>, IEnumerable<VendasProdutosViewModel>>(_vendasProdutosService.GetAll());
        }

        public IEnumerable<VendasProdutosViewModel> GetByVenda(Guid vendaId)
        {
            return Mapper.Map<IEnumerable<VendasProdutos>, IEnumerable<VendasProdutosViewModel>>(_vendasProdutosService.GetByVenda(vendaId));
        }

        public VendasProdutosViewModel GetById(Guid id)
        {
            return Mapper.Map<VendasProdutos, VendasProdutosViewModel>(_vendasProdutosService.GetById(id));
        }

        public void AddVendasProdutos(IEnumerable<VendasProdutosViewModel> vendasProdutosViewModelList, Guid idVenda)
        {
            var vendasProdutosList = Mapper.Map<IEnumerable<VendasProdutosViewModel>, IEnumerable<VendasProdutos>>(vendasProdutosViewModelList);

            BeginTransaction();
            _vendasProdutosService.AddVendasProdutos(vendasProdutosList, idVenda);

            //Atualiza Estoque
            foreach (var item in vendasProdutosList)
            {
                var e = _estoqueService.GetById(item.EstoqueId);
                e.Quantidade = e.Quantidade - item.Quantidade;
                e.SaldoVenda = e.SaldoVenda + (item.Quantidade * e.ValorUnitario);
                e.TotalVendido = e.TotalVendido + item.Quantidade;
                _estoqueService.Add(e);
            }

            Commit();
        }

        public void AddProduto(VendasProdutosViewModel vendasProdutosViewModel)
        {
            var vendaProduto = Mapper.Map<VendasProdutosViewModel, VendasProdutos>(vendasProdutosViewModel);

            BeginTransaction();
            _vendasProdutosService.Add(vendaProduto);

            //Atualiza Estoque
            var e = _estoqueService.GetById(vendaProduto.EstoqueId);
            e.Quantidade = e.Quantidade - vendaProduto.Quantidade;
            e.SaldoVenda = e.SaldoVenda + (vendaProduto.Quantidade * e.ValorUnitario);
            e.TotalVendido = e.TotalVendido + vendaProduto.Quantidade;

            _estoqueService.Update(e);

            //Atualiza total venda
            var venda = _vendasService.GetById(vendaProduto.VendaId);
            venda.ValorTotal = venda.ValorTotal + (vendaProduto.Quantidade * e.ValorUnitario);            

            _vendasService.Update(venda);

            //Atualiza a Receita
            var r = _receitasService.GetById(venda.ReceitasId);
            r.Valor = r.Valor + (vendaProduto.Quantidade * e.ValorUnitario);
            r.ValorTotal = r.Valor;

            _receitasService.Update(r);

            Commit();
        }

        public void Remove(Guid id)
        {
            var vendasProdutos = Mapper.Map<VendasProdutosViewModel, VendasProdutos>(GetById(id));

            BeginTransaction();
            _vendasProdutosService.Remove(vendasProdutos);

            //Retorna quantidade ao Estoque e atualiza saldo
            var e = _estoqueService.GetById(vendasProdutos.EstoqueId);
            e.Quantidade = e.Quantidade + vendasProdutos.Quantidade;
            e.SaldoVenda = e.SaldoVenda - (vendasProdutos.Quantidade * e.ValorUnitario);
            e.TotalVendido = e.TotalVendido - vendasProdutos.Quantidade;

            _estoqueService.Add(e);

            //Atualiza valor Total Venda
            var venda = _vendasService.GetById(vendasProdutos.VendaId);
            venda.ValorTotal = venda.ValorTotal - (vendasProdutos.Quantidade * e.ValorUnitario);
            _vendasService.Update(venda);

            //Atualiza valor Receita
            var r = _receitasService.GetById(venda.ReceitasId);
            r.Valor = r.Valor - (vendasProdutos.Quantidade * e.ValorUnitario);
            r.ValorTotal = r.Valor;

            _receitasService.Update(r);

            //_logSistemaService.AddLog("Valores", id, "Cadastro", "QtdDias:" + valores.QtdDias + " - " + valores.Valor);
            Commit();
        }

        public void Dispose()
        {
            _vendasProdutosService.Dispose();
        }
    }
}
