using System.Collections.Generic;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Infra.Data.Context;
using AutoMapper;
using BarraFisik.Domain.Entities;
using System;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Application.App
{
    public class VendasAppService : AppServiceBase<BarraFisikContext>, IVendasAppService
    {
        private readonly IVendasService _vendasService;
        private readonly IVendasProdutosService _vendasProdutosService;
        private readonly IEstoqueService _estoqueService;
        private readonly IReceitasService _receitasService;
        private readonly ILogSistemaService _logSistemaService;

        public VendasAppService(IVendasService vendasService, ILogSistemaService logSistemaService, IVendasProdutosService vendasProdutosService, IReceitasService receitasService, IEstoqueService estoqueService)
        {
            _vendasService = vendasService;
            _logSistemaService = logSistemaService;
            _vendasProdutosService = vendasProdutosService;
            _receitasService = receitasService;
            _estoqueService = estoqueService;
        }


        public void Add(VendasViewModel vendasViewModel)
        {
            var venda = Mapper.Map<VendasViewModel, Vendas>(vendasViewModel);

            BeginTransaction();
            venda.DataVenda = DateTime.Now;
            _vendasService.Add(venda);

            _logSistemaService.AddLog("Vendas", vendasViewModel.VendaId, "Cadastro", "");
            Commit();
        }

        public VendasViewModel GetById(Guid id)
        {
            return Mapper.Map<Vendas, VendasViewModel>(_vendasService.GetById(id));
        }

        public IEnumerable<VendasViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Vendas>, IEnumerable<VendasViewModel>>(_vendasService.GetAll());
        }

        public IEnumerable<VendasViewModel> GetVendas()
        {
            return Mapper.Map<IEnumerable<Vendas>, IEnumerable<VendasViewModel>>(_vendasService.GetVendas());
        }

        public IEnumerable<VendasViewModel> GetPendentes(int mes, int ano)
        {
            return Mapper.Map<IEnumerable<Vendas>, IEnumerable<VendasViewModel>>(_vendasService.GetPendentes(mes, ano));
        }

        public IEnumerable<VendasViewModel> SearchVendas(SearchVendasViewModel sv)
        {
            var search = Mapper.Map<SearchVendasViewModel, SearchVendas>(sv);
            return Mapper.Map<IEnumerable<Vendas>, IEnumerable<VendasViewModel>>(_vendasService.SearchVendas(search));
        }

        public IEnumerable<VendasViewModel> GetVendasByCliente(Guid idCliente)
        {
            return Mapper.Map<IEnumerable<Vendas>, IEnumerable<VendasViewModel>>(_vendasService.GetVendasByCliente(idCliente));
        }

        public void Update(VendasViewModel vendasViewModel)
        {
            var venda = Mapper.Map<VendasViewModel, Vendas>(vendasViewModel);

            BeginTransaction();
            _vendasService.Add(venda);

            //Atualiza dados na receita
            var r = _receitasService.GetById(venda.ReceitasId);
            r.DataVencimento = venda.DataVencimento;
            r.DataPagamento = venda.DataPagamento;
            r.TipoPagamentoId = venda.TipoPagamentoId;

            r.Situacao = r.DataPagamento != null ? "Quitado" : "Pendente";
            _receitasService.Add(r);

            _logSistemaService.AddLog("Venda", vendasViewModel.VendaId, "Update", "");
            Commit();
        }

        public void Remove(Guid id)
        {
            var venda = Mapper.Map<VendasViewModel, Vendas>(GetById(id));

            BeginTransaction();

            _vendasService.Remove(venda);

            //Atualiza o estoque e Deleta a VendaProduto
            foreach (var item in _vendasProdutosService.GetByVenda(id))
            {               
                var e = _estoqueService.GetById(item.EstoqueId);
                e.Quantidade = e.Quantidade + item.Quantidade;
                e.SaldoVenda =  e.SaldoVenda - (item.Quantidade * e.ValorUnitario);
                e.TotalVendido = e.TotalVendido - item.Quantidade;

                _estoqueService.AtualizaProdutos(e);

                _vendasProdutosService.Remove(item);
            }

            //Delete Receita Financeira
            _receitasService.Remove(_receitasService.GetById(venda.ReceitasId));

            _logSistemaService.AddLog("Venda", venda.VendaId, "Remove", "");
            Commit();
        }

        public IList<int> GetVendasAnual(int ano)
        {
            return _vendasService.GetVendasAnual(ano);
        }

        

        public void Dispose()
        {
            _vendasService.Dispose();
        }
    }
}
