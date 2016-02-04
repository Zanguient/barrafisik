using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.ValueObjects;
using Dapper;

namespace BarraFisik.Infra.Data.Repository.ReadOnly
{
    public class ReceitasRepositoryReadOnly : RepositoryBaseReadOnly, IReceitasRepositoryReadOnly
    {
        public IEnumerable<Receitas> GetReceitas()
        {
            using (var cn = Connection)
            {
                cn.Open();

                //var sql = @"select  *, CONVERT(varchar(10), r.Data, 103) as DataReceita
                //                from Receitas r
                //                inner join CategoriaFinanceira cf on r.CategoriaFinanceiraId = cf.CategoriaFinanceiraId";

                //var sql = @"select 
                //             CONVERT(varchar(10), r.Data, 103) as DataReceita, 
                //             r.Data as Data,
                //             r.Nome as Nome, 
                //             r.Observacao as Observacao, 
                //             r.Valor as Valor, 
                //             cf.Categoria as Categoria,
                //             r.ReceitasId as ReceitasId,
                //             '' as Cliente,
                //             cf.CategoriaFinanceiraId as CategoriaFinanceiraId
                //            from Receitas r
                //            inner join CategoriaFinanceira cf on r.CategoriaFinanceiraId = cf.CategoriaFinanceiraId
                //            union
                //            select 
                //             CONVERT(varchar(10), af.DataPagamento, 103) as DataReceita, 
                //             af.DataPagamento as Data,
                //             af.Nome as Nome, 
                //             '' as Observacao, 
                //             af.Valor as Valor, 
                //             cf.Categoria as Categoria,
                //             af.ReceitasAvaliacaoFisicaId as ReceitasId,
                //             c.Nome as Cliente,
                //             cf.CategoriaFinanceiraId as CategoriaFinanceiraId
                //            from ReceitasAvaliacoesFisicas af
                //            inner join CategoriaFinanceira cf on af.CategoriaFinanceiraId = cf.CategoriaFinanceiraId
                //            left join Cliente c on af.ClienteId = c.ClienteId
                //            union
                //            select 
                //             CONVERT(varchar(10), m.DataPagamento, 103) as DataReceita, 
                //             m.DataPagamento as Data,
                //             m.Nome as Nome, 
                //             '' as Observacao, 
                //             m.ValorPago as Valor, 
                //             cf.Categoria as Categoria,
                //             m.MensalidadesId as MensalidadesId,
                //             c.Nome as Cliente,
                //             cf.CategoriaFinanceiraId as CategoriaFinanceiraId
                //            from Mensalidades m
                //            inner join CategoriaFinanceira cf on m.CategoriaFinanceiraId = cf.CategoriaFinanceiraId
                //            left join Cliente c on m.ClienteId = c.ClienteId";

                var sql = @"select * from Receitas r 
                                inner join CategoriaFinanceira cf on r.CategoriaFinanceiraId = cf.CategoriaFinanceiraId
                                left join TipoPagamento tp on r.TipoPagamentoId = tp.TipoPagamentoId
                                where Month(r.DataVencimento) = Month(GetDate()) and YEAR(r.DataVencimento) = YEAR(getDate()) ";

                var receitas = cn.Query<Receitas, CategoriaFinanceira, TipoPagamento, Receitas>(
                    sql,
                    (r, cf, tp) =>
                    {
                        r.CategoriaFinanceiraId = cf.CategoriaFinanceiraId;
                        r.CategoriaFinanceira = cf;
                        r.TipoPagamento = tp;
                        return r;
                    }, splitOn: "ReceitasId, CategoriaFinanceiraId, TipoPagamentoId");

                cn.Close();
                return receitas;
            }
        }

        public IEnumerable<Receitas> SearchReceitas(SearchReceita sr)
        {
            using (var cn = Connection)
            {
                cn.Open();
                bool hasData = false;
                var sql = @"select * from Receitas r 
                                inner join CategoriaFinanceira cf on r.CategoriaFinanceiraId = cf.CategoriaFinanceiraId
                                left join TipoPagamento tp on r.TipoPagamentoId = tp.TipoPagamentoId
                                where 1 = 1";

                var dt = new DateTime();

                if (sr.EmissaoInicio != dt)
                {
                    sql = sql + " AND r.DataEmissao >= '" + sr.EmissaoInicio.ToString("yyyy-MM-dd 00:00:00") + "'";
                    hasData = true;
                }

                if (sr.EmissaoFim != dt)
                {
                    sql = sql + " AND r.DataEmissao <= '" + sr.EmissaoFim.ToString("yyyy-MM-dd 23:59:59") + "'";
                    hasData = true;
                }


                if (sr.PagamentoInicio != dt)
                {
                    sql = sql + " AND r.DataPagamento >= '" + sr.PagamentoInicio.ToString("yyyy-MM-dd 00:00:00") + "'";
                    hasData = true;
                }

                if (sr.PagamentoFim != dt)
                {
                    sql = sql + " AND r.DataPagamento <= '" + sr.PagamentoFim.ToString("yyyy-MM-dd 23:59:59") + "'";
                    hasData = true;
                }


                if (sr.VencimentoInicio != dt)
                {
                    sql = sql + " AND r.DataVencimento >= '" + sr.VencimentoInicio.ToString("yyyy-MM-dd 00:00:00") + "'";
                    hasData = true;
                }

                if (sr.VencimentoFim != dt)
                {
                    sql = sql + " AND r.DataVencimento <= '" + sr.VencimentoFim.ToString("yyyy-MM-dd 23:59:59") + "'";
                    hasData = true;
                }

                if (!hasData)
                    sql = sql + " AND Month(r.DataVencimento) = Month(GetDate()) and YEAR(r.DataVencimento) = YEAR(getDate())";

                var receitas = cn.Query<Receitas, CategoriaFinanceira, TipoPagamento, Receitas>(
                    sql,
                    (r, cf, tp) =>
                    {
                        r.CategoriaFinanceiraId = cf.CategoriaFinanceiraId;
                        r.CategoriaFinanceira = cf;
                        r.TipoPagamento = tp;
                        return r;
                    }, splitOn: "DespesasId, CategoriaFinanceiraId, TipoPagamentoId");


                cn.Close();
                return receitas;
            }
        }
    }
}