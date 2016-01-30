using System;
using System.Collections.Generic;
using AutoMapper;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Domain.ValueObjects;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Application.App
{
    public class DespesasAppService : AppServiceBase<BarraFisikContext>, IDespesasAppService
    {
        private readonly IDespesasService _despesasService;
        private readonly ILogReceitasDespesasService _logReceitasDespesasService;

        public DespesasAppService(IDespesasService despesasService, ILogReceitasDespesasService logReceitasDespesasService)
        {
            _despesasService = despesasService;
            _logReceitasDespesasService = logReceitasDespesasService;
        }

        public void Add(DespesasViewModel despesasViewModel)
        {
            var despesas = Mapper.Map<DespesasViewModel, Despesas>(despesasViewModel);

            BeginTransaction();
            despesas.DataEmissao = DateTime.Now;
            _despesasService.Add(despesas);

            //Log
            _logReceitasDespesasService.AddLog("Cadastro", GetLog(despesas));
            Commit();
        }

        public DespesasViewModel GetById(Guid id)
        {
            return Mapper.Map<Despesas, DespesasViewModel>(_despesasService.GetById(id));
        }

        public IEnumerable<DespesasViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Despesas>, IEnumerable<DespesasViewModel>>(_despesasService.GetAll());
        }

        public IEnumerable<DespesasViewModel> GetDespesas()
        {
            return Mapper.Map<IEnumerable<Despesas>, IEnumerable<DespesasViewModel>>(_despesasService.GetDespesas());
        }

        public IEnumerable<DespesasViewModel> GetDespesasAll()
        {
            return Mapper.Map<IEnumerable<Despesas>, IEnumerable<DespesasViewModel>>(_despesasService.GetDespesasAll());
        }

        public IEnumerable<DespesasViewModel> SearchDespesas(SearchDespesasViewModel searchViewModel)
        {
            var search = Mapper.Map<SearchDespesasViewModel, SearchDespesa>(searchViewModel);
            return Mapper.Map<IEnumerable<Despesas>, IEnumerable<DespesasViewModel>>(_despesasService.SearchDespesas(search));
        }

        public void Update(DespesasViewModel despesasViewModel)
        {
            var despesas = Mapper.Map<DespesasViewModel, Despesas>(despesasViewModel);

            BeginTransaction();
            _despesasService.Add(despesas);

            ////Log
            _logReceitasDespesasService.AddLog("Update", GetLog(despesas));

            Commit();
        }

        public void Remove(Guid id)
        {
            var despesas = Mapper.Map<DespesasViewModel, Despesas>(GetById(id));

            BeginTransaction();
            _despesasService.Remove(despesas);

            //Log       
            _logReceitasDespesasService.AddLog("Remove", GetLog(despesas));
            Commit();
        }

        public void Dispose()
        {
            _despesasService.Dispose();
        }

        private static LogReceitasDespesas GetLog(Despesas d)
        {
            var logRecDesp = new LogReceitasDespesas
            {
                Documento = d.Documento,
                DataVencimento = d.DataVencimento,
                DataPagamento = d.DataPagamento,
                DataEmissao = d.DataEmissao,
                Valor = d.Valor,
                Juros = d.Juros,
                Multa = d.Multa,
                ValorTotal = d.ValorTotal,
                Observacao = d.Observacao,
                Situacao = d.Situacao,
                CategoriaFinanceiraId = d.CategoriaFinanceiraId.ToString(),
                FornecedorId = d.FornecedorId.ToString(),
                FuncionarioId = d.FuncionarioId.ToString(),                
                TipoPagamentoId = d.TipoPagamentoId,
                RegistroId = d.DespesasId.ToString()
            };

            return logRecDesp;
        }

        
    }
}