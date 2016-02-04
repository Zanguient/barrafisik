using System;
using System.Collections.Generic;
using AutoMapper;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Infra.Data.Context;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Application.App
{
    public class ReceitasAppService : AppServiceBase<BarraFisikContext>, IReceitasAppService
    {
        private readonly IReceitasService _receitasService;
        private readonly ILogReceitasDespesasService _logReceitasDespesasService;

        public ReceitasAppService(IReceitasService receitasService, ILogReceitasDespesasService logReceitasDespesasService)
        {
            _receitasService = receitasService;
            _logReceitasDespesasService = logReceitasDespesasService;
        }

        public void Add(ReceitasViewModel receitasViewModel)
        {
            var receita = Mapper.Map<ReceitasViewModel, Receitas>(receitasViewModel);

            BeginTransaction();
            receita.DataEmissao = DateTime.Now;
            _receitasService.Add(receita);

            ////Log
            _logReceitasDespesasService.AddLog("Cadastro", GetLog(receita));
            Commit();
        }

        public ReceitasViewModel GetById(Guid id)
        {
            return Mapper.Map<Receitas, ReceitasViewModel>(_receitasService.GetById(id));
        }

        public IEnumerable<ReceitasViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Receitas>, IEnumerable<ReceitasViewModel>>(_receitasService.GetAll());
        }

        public IEnumerable<ReceitasViewModel> GetReceitas()
        {
            return Mapper.Map<IEnumerable<Receitas>, IEnumerable<ReceitasViewModel>>(_receitasService.GetReceitas());
        }

        public IEnumerable<ReceitasViewModel> SearchReceitas(SearchReceitasViewModel sr)
        {
            var search = Mapper.Map<SearchReceitasViewModel, SearchReceita>(sr);
            return Mapper.Map<IEnumerable<Receitas>, IEnumerable<ReceitasViewModel>>(_receitasService.SearchReceitas(search));
        }

        public void Update(ReceitasViewModel receitasViewModel)
        {
            var receita = Mapper.Map<ReceitasViewModel, Receitas>(receitasViewModel);

            BeginTransaction();            
            _receitasService.Add(receita);

            //Log
            _logReceitasDespesasService.AddLog("Cadastro", GetLog(receita));
            Commit();
        }

        public void Remove(Guid id)
        {
            var receita = Mapper.Map<ReceitasViewModel, Receitas>(GetById(id));

            BeginTransaction();
            _receitasService.Remove(receita);

            //Log
            _logReceitasDespesasService.AddLog("Cadastro", GetLog(receita));
            Commit();
        }

        public void Dispose()
        {
            _receitasService.Dispose();
        }

        private static LogReceitasDespesas GetLog(Receitas r)
        {
            var logRecDesp = new LogReceitasDespesas
            {
                Documento = r.Documento,
                DataVencimento = r.DataVencimento,
                DataPagamento = r.DataPagamento,
                DataEmissao = r.DataEmissao,
                Valor = r.Valor,
                Juros = r.Juros,
                Multa = r.Multa,
                ValorTotal = r.ValorTotal,
                Observacao = r.Observacao,
                Situacao = r.Situacao,
                Tipo = "Receita",
                CategoriaFinanceiraId = r.CategoriaFinanceiraId.ToString(),
                FornecedorId = r.ClienteId.ToString(),
                FuncionarioId = r.FuncionarioId.ToString(),
                TipoPagamentoId = r.TipoPagamentoId,
                RegistroId = r.ReceitasId.ToString()
            };

            return logRecDesp;
        }

       
    }
}