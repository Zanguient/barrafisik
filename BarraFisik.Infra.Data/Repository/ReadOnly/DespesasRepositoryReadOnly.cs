using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using Dapper;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Infra.Data.Repository.ReadOnly
{
    public class DespesasRepositoryReadOnly : RepositoryBaseReadOnly, IDespesasRepositoryReadOnly
    {        
        public IEnumerable<Despesas> GetDespesas()
        {
            using (var cn = Connection)
            {
                cn.Open();

                var sql = @"select * from Despesas d 
                                inner join CategoriaFinanceira cf on d.CategoriaFinanceiraId = cf.CategoriaFinanceiraId
                                left join Fornecedores f on d.FornecedorId = f.FornecedorId
                                left join TipoPagamento tp on d.TipoPagamentoId = tp.TipoPagamentoId
                                left join SubCategoriaFinanceira sc on d.SubCategoriaFinanceiraId = sc.SubCategoriaFinanceiraId
                                where Month(d.DataVencimento) = Month(GetDate()) and YEAR(d.DataVencimento) = YEAR(getDate())";

                var despesas = cn.Query<Despesas, CategoriaFinanceira, Fornecedores, TipoPagamento, SubCategoriaFinanceira, Despesas>(
                    sql,
                    (d, cf, f, tp, sc) =>
                    {
                        d.CategoriaFinanceiraId = cf.CategoriaFinanceiraId;
                        d.Fornecedores = f;
                        d.CategoriaFinanceira = cf;
                        d.TipoPagamento = tp;
                        d.SubCategoriaFinanceira = sc;
                        return d;
                    }, splitOn: "DespesasId, CategoriaFinanceiraId, FornecedorId, TipoPagamentoId, SubCategoriaFinanceiraId");


                cn.Close();
                return despesas;
            }
        }

        public IEnumerable<Despesas> SearchDespesas(SearchDespesa sd)
        {
            using (var cn = Connection)
            {
                cn.Open();
                bool hasData = false;
                var sql = @"select * from Despesas d 
                                inner join CategoriaFinanceira cf on d.CategoriaFinanceiraId = cf.CategoriaFinanceiraId
                                left join Fornecedores f on d.FornecedorId = f.FornecedorId
                                left join TipoPagamento tp on d.TipoPagamentoId = tp.TipoPagamentoId
                                left join SubCategoriaFinanceira sc on d.SubCategoriaFinanceiraId = sc.SubCategoriaFinanceiraId
                                where 1 = 1";

                var dt = new DateTime();

                if (sd.EmissaoInicio != dt)
                {
                    sql = sql + " AND d.DataEmissao >= '" + sd.EmissaoInicio.ToString("yyyy-MM-dd 00:00:00") + "'";
                    hasData = true;
                }
                    
                if (sd.EmissaoFim != dt)
                {
                    sql = sql + " AND d.DataEmissao <= '" + sd.EmissaoFim.ToString("yyyy-MM-dd 23:59:59") + "'";
                    hasData = true;
                }
                    

                if (sd.PagamentoInicio != dt)
                {
                    sql = sql + " AND d.DataPagamento >= '" + sd.PagamentoInicio.ToString("yyyy-MM-dd 00:00:00") + "'";
                    hasData = true;
                }
                    
                if (sd.PagamentoFim != dt)
                {
                    sql = sql + " AND d.DataPagamento <= '" + sd.PagamentoFim.ToString("yyyy-MM-dd 23:59:59") + "'";
                    hasData = true;
                }
                    

                if (sd.VencimentoInicio != dt)
                {
                    sql = sql + " AND d.DataVencimento >= '" + sd.VencimentoInicio.ToString("yyyy-MM-dd 00:00:00") + "'";
                    hasData = true;
                }
                    
                if (sd.VencimentoFim != dt)
                {
                    sql = sql + " AND d.DataVencimento <= '" + sd.VencimentoFim.ToString("yyyy-MM-dd 23:59:59") + "'";
                    hasData = true;
                }

                if (!hasData)
                    sql = sql + " AND Month(d.DataVencimento) = Month(GetDate()) and YEAR(d.DataVencimento) = YEAR(getDate())";

                var despesas = cn.Query<Despesas, CategoriaFinanceira, Fornecedores, TipoPagamento, SubCategoriaFinanceira, Despesas >(
                    sql,
                    (d, cf, f, tp, sc) =>
                    {
                        d.CategoriaFinanceiraId = cf.CategoriaFinanceiraId;
                        d.Fornecedores = f;
                        d.CategoriaFinanceira = cf;
                        d.TipoPagamento = tp;
                        d.SubCategoriaFinanceira = sc;
                        return d;
                    }, splitOn: "DespesasId, CategoriaFinanceiraId, FornecedorId, TipoPagamentoId, SubCategoriaFinanceiraId");


                cn.Close();
                return despesas;
            }
        }
    }
}