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

                //var sql = @"select  *, CONVERT(varchar(10), d.Data, 103) as DataDespesa
                //                from Despesas d
                //                inner join CategoriaFinanceira cf on d.CategoriaFinanceiraId = cf.CategoriaFinanceiraId";

                var sql = @"select * from Despesas d 
                                inner join CategoriaFinanceira cf on d.CategoriaFinanceiraId = cf.CategoriaFinanceiraId
                                left join TipoPagamento tp on d.TipoPagamentoId = tp.TipoPagamentoId
                                where Month(d.DataVencimento) = Month(GetDate()) and YEAR(d.DataVencimento) = YEAR(getDate())";

                var despesas = cn.Query<Despesas, CategoriaFinanceira, TipoPagamento, Despesas>(
                    sql,
                    (d, cf, tp) =>
                    {
                        d.CategoriaFinanceiraId = cf.CategoriaFinanceiraId;
                        d.CategoriaFinanceira = cf;
                        d.TipoPagamento = tp;
                        return d;
                    }, splitOn: "DespesasId, CategoriaFinanceiraId, TipoPagamentoId");


                cn.Close();
                return despesas;
            }
        }

        public IEnumerable<Despesas> SearchDespesas(SearchDespesa sd)
        {
            using (var cn = Connection)
            {
                cn.Open();

                var sql = @"select * from Despesas d 
                                inner join CategoriaFinanceira cf on d.CategoriaFinanceiraId = cf.CategoriaFinanceiraId
                                left join TipoPagamento tp on d.TipoPagamentoId = tp.TipoPagamentoId
                                where 1 = 1";

                var dt = new DateTime();

                if (sd.EmissaoInicio != dt)
                    sql = sql + " AND d.DataEmissao >= '" + sd.EmissaoInicio.ToString("yyyy-MM-dd 00:00:00") + "'";
                if (sd.EmissaoFim != dt)
                    sql = sql + " AND d.DataEmissao <= '" + sd.EmissaoFim.ToString("yyyy-MM-dd 23:59:59") + "'";

                if (sd.PagamentoInicio != dt)
                    sql = sql + " AND d.DataPagamento >= '" + sd.PagamentoInicio.ToString("yyyy-MM-dd 00:00:00") + "'";
                if (sd.PagamentoFim != dt)
                    sql = sql + " AND d.DataPagamento <= '" + sd.PagamentoFim.ToString("yyyy-MM-dd 23:59:59") + "'";

                if (sd.VencimentoInicio != dt)
                    sql = sql + " AND d.DataVencimento >= '" + sd.VencimentoInicio.ToString("yyyy-MM-dd 00:00:00") + "'";
                if (sd.VencimentoFim != dt)
                    sql = sql + " AND d.DataVencimento <= '" + sd.VencimentoFim.ToString("yyyy-MM-dd 23:59:59") + "'";

                var despesas = cn.Query<Despesas, CategoriaFinanceira, TipoPagamento, Despesas>(
                    sql,
                    (d, cf, tp) =>
                    {
                        d.CategoriaFinanceiraId = cf.CategoriaFinanceiraId;
                        d.CategoriaFinanceira = cf;
                        d.TipoPagamento = tp;
                        return d;
                    }, splitOn: "DespesasId, CategoriaFinanceiraId, TipoPagamentoId");


                cn.Close();
                return despesas;
            }
        }
    }
}