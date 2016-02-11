﻿using System.Collections.Generic;
using System.Data;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.ValueObjects;
using Dapper;
using System;

namespace BarraFisik.Infra.Data.Repository.ReadOnly
{
    public class RelatorioFinanceiroRepositoryReadOnly : RepositoryBaseReadOnly, IRelatorioFinanceiroRepositoryReadOnly
    {
        public IEnumerable<RelatorioFinanceiro> GetRelatorio(RelatorioFinanceiroSearch filters)
        {
            using (var cn = Connection)
            {
                var dt = new DateTime();
                var query = @"
                                select 
	                                r.Documento,
	                                r.DataVencimento,
	                                r.DataEmissao,
	                                r.DataPagamento,
	                                r.Valor,
	                                r.ValorTotal,
	                                cf.Tipo,
	                                r.Situacao,
	                                cf.Categoria,
	                                sc.SubCategoria,
                                    r.Observacao
                                from Receitas r
                                left join SubCategoriaFinanceira sc on r.SubCategoriaFinanceiraId = sc.SubCategoriaFinanceiraId
                                inner join CategoriaFinanceira cf on r.CategoriaFinanceiraId = cf.CategoriaFinanceiraId
                                where 1 = 1";


                                if (filters.Tipo != null && filters.Tipo != "Todos")
                                    query = query + " and cf.Tipo = '" + filters.Tipo + "' ";
                                if (filters.EmissaoInicio != dt)
                                    query = query + " and r.DataEmissao >= '" + filters.EmissaoInicio.ToString("yyyy-MM-dd 00:00:00") + "' ";
                                if (filters.EmissaoFim != dt)
                                    query = query + " and r.DataEmissao <= '" + filters.EmissaoFim.ToString("yyyy-MM-dd 23:59:59") + "' ";
                                if (filters.VencimentoInicio != dt)
                                    query = query + " and r.DataVencimento >= '" + filters.VencimentoInicio.ToString("yyyy-MM-dd 00:00:00") + "' ";
                                if (filters.VencimentoFim != dt)
                                    query = query + " and r.DataVencimento <= '" + filters.VencimentoFim.ToString("yyyy-MM-dd 23:59:59") + "' ";
                                if (filters.PagamentoInicio != dt)
                                    query = query + " and r.DataPagamento >= '" + filters.PagamentoInicio.ToString("yyyy-MM-dd 00:00:00") + "' ";
                                if (filters.PagamentoFim != dt)
                                    query = query + " and r.DataPagamento <= '" + filters.PagamentoFim.ToString("yyyy-MM-dd 23:59:59") + "' ";
                                if (filters.CategoriaId != null)
                                    query = query + " and cf.CategoriaFinanceiraId = '" + filters.CategoriaId + "' ";
                                if (filters.SubCategoriaId != null)
                                    query = query + " and sc.SubCategoriaFinanceiraId = '" + filters.SubCategoriaId + "' ";
                                if (filters.Situacao != "")
                                    query = query + " and r.Situacao = '" + filters.Situacao + "' ";


                                query = query + 
                                "union                                                                                                "+
                                "   select                                                                                            "+
	                            "       d.Documento,                                                                                  "+
	                            "       d.DataVencimento,                                                                             "+
	                            "       d.DataEmissao,                                                                                "+
	                            "       d.DataPagamento,                                                                              "+
	                            "       d.Valor,                                                                                      "+
	                            "       d.ValorTotal,                                                                                 "+
	                            "       cf.Tipo,                                                                                      "+
	                            "       d.Situacao,                                                                                   "+
	                            "       cf.Categoria,                                                                                 "+
	                            "       sc.SubCategoria,                                                                              "+
                                "       d.Observacao                                                                                  "+
                                "   from Despesas d                                                                                   "+
                                "   left join SubCategoriaFinanceira sc on d.SubCategoriaFinanceiraId = sc.SubCategoriaFinanceiraId   "+
                                "   inner join CategoriaFinanceira cf on d.CategoriaFinanceiraId = cf.CategoriaFinanceiraId           "+
                                "   where 1 = 1                                                                                       ";

                                if (filters.Tipo != null && filters.Tipo != "Todos")
                                    query = query + " and cf.Tipo = '" + filters.Tipo + "' ";
                                if (filters.EmissaoInicio != dt)
                                    query = query + " and d.DataEmissao >= '" + filters.EmissaoInicio.ToString("yyyy-MM-dd 00:00:00") + "' ";
                                if (filters.EmissaoFim != dt)
                                    query = query + " and d.DataEmissao <= '" + filters.EmissaoFim.ToString("yyyy-MM-dd 23:59:59") + "' ";
                                if (filters.VencimentoInicio != dt)
                                    query = query + " and d.DataVencimento >= '" + filters.VencimentoInicio.ToString("yyyy-MM-dd 00:00:00") + "' ";
                                if (filters.VencimentoFim != dt)
                                    query = query + " and d.DataVencimento <= '" + filters.VencimentoFim.ToString("yyyy-MM-dd 23:59:59") + "' ";
                                if (filters.PagamentoInicio != dt)
                                    query = query + " and d.DataPagamento >= '" + filters.PagamentoInicio.ToString("yyyy-MM-dd 00:00:00") + "' ";
                                if (filters.PagamentoFim != dt)
                                    query = query + " and d.DataPagamento <= '" + filters.PagamentoFim.ToString("yyyy-MM-dd 23:59:59") + "' ";
                                if (filters.CategoriaId != null)
                                    query = query + " and cf.Categoria = '" + filters.CategoriaId + "' ";
                                if (filters.SubCategoriaId != null)
                                    query = query + " and sc.SubCategoria = '" + filters.SubCategoriaId + "' ";
                                if (filters.Situacao != "")
                                    query = query + " and d.Situacao = '" + filters.Situacao + "' ";
                cn.Open();
                var relatorio = cn.Query<RelatorioFinanceiro>(query);
                cn.Close();

                return relatorio;
            }
        }

        public IEnumerable<RelatorioFinanceiroTotalMeses> GetTotalPorMes()
        {
            
            using (var cn = Connection)
            {
                var query = @"select distinct  (select sum(Valor) from Despesas where MONTH(Data) = 1) as  DespesasJaneiro, " +
                            "  (select sum(Valor) from Despesas where MONTH(Data) = 2) as  DespesasFevereiro, " +
                            "  (select sum(Valor) from Despesas where MONTH(Data) = 3) as  DespesasMarco, " +
                            "  (select sum(Valor) from Despesas where MONTH(Data) = 4) as  DespesasAbril, " +
                            "  (select sum(Valor) from Despesas where MONTH(Data) = 5) as  DespesasMaio, " +
                            "  (select sum(Valor) from Despesas where MONTH(Data) = 6) as  DespesasJunho, " +
                            "  (select sum(Valor) from Despesas where MONTH(Data) = 7) as  DespesasJulho, " +
                            "  (select sum(Valor) from Despesas where MONTH(Data) = 8) as  DespesasAgosto, " +
                            "  (select sum(Valor) from Despesas where MONTH(Data) = 9) as  DespesasSetembro, " +
                            "  (select sum(Valor) from Despesas where MONTH(Data) = 10) as DespesasOutubro, " +
                            "  (select sum(Valor) from Despesas where MONTH(Data) = 11) as DespesasNovembro," +
                            "  (select sum(Valor) from Despesas where MONTH(Data) = 12) as DespesasDezembro";
                

                cn.Open();
                var relatorio = cn.Query<RelatorioFinanceiroTotalMeses>(query);
                cn.Close();

                return relatorio;
            }
        }
    }
}