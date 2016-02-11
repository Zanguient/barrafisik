using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.ValueObjects;
using Dapper;
using System.Linq;

namespace BarraFisik.Infra.Data.Repository.ReadOnly
{
    public class ReceitasRepositoryReadOnly : RepositoryBaseReadOnly, IReceitasRepositoryReadOnly
    {
        public bool ExisteMensalidade(Receitas mensalidade)
        {
            using (var cn = Connection)
            {
                var query = @"  SELECT CASE WHEN EXISTS (
                                SELECT *
                                FROM Receitas r 
                                WHERE	r.ClienteId = '" + mensalidade.ClienteId + "' and " +
                                        "r.AnoReferencia = '" + mensalidade.AnoReferencia + "' and " +
                                        "r.MesReferencia = '" + mensalidade.MesReferencia + "' and " +
                                        "r.ReceitasId != '" + mensalidade.ReceitasId + "' " +
                                ") THEN CAST(1 AS INT) ELSE CAST(0 AS INT) END ";

                cn.Open();
                var valido = cn.Query<int>(query).First();
                cn.Close();
                if (valido == 1)
                {
                    return true;
                }
                return false;
            }
        }

        public IEnumerable<Receitas> GetMensalidadesCliente(Guid? idCliente)
        {
            using (var cn = Connection)
            {
                var query = @"  Select distinct * from Receitas r 
                                left join TipoPagamento tp on r.TipoPagamentoId = tp.TipoPagamentoId
                                where r.ClienteId = '" + idCliente + 
                                "' and r.CategoriaFinanceiraId = '1c1278df-f5a5-4407-a0c4-bdbb71c362b1' and r.SubCategoriaFinanceiraId = '0d57c87d-3bd9-420b-ab98-123fdb75a269'";

                cn.Open();
                var receitas = cn.Query<Receitas, TipoPagamento, Receitas>(
                    query,
                    (r, tp) => {
                        r.TipoPagamento = tp;
                        return r;
                    }, splitOn: "ReceitasId, TipoPagamentoId");
                cn.Close();

                return receitas;
            }
        }

        public IEnumerable<Receitas> GetAvaliacaoCliente(Guid? idCliente)
        {
            using (var cn = Connection)
            {
                var query = @"  Select distinct * from Receitas r 
                                left join TipoPagamento tp on r.TipoPagamentoId = tp.TipoPagamentoId
                                where r.ClienteId = '" + idCliente +
                                "' and r.CategoriaFinanceiraId = '1c1278df-f5a5-4407-a0c4-bdbb71c362b1' and r.SubCategoriaFinanceiraId = 'ecaac024-15bd-4ee0-8422-07d809bb1be9'";

                cn.Open();
                var receitas = cn.Query<Receitas, TipoPagamento, Receitas>(
                    query,
                    (r, tp) => {
                        r.TipoPagamento = tp;
                        return r;
                    }, splitOn: "ReceitasId, TipoPagamentoId");
                cn.Close();

                return receitas;
            }
        }

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
                                left join SubCategoriaFinanceira sc on r.SubCategoriaFinanceiraId = sc.SubCategoriaFinanceiraId
                                where Month(r.DataVencimento) = Month(GetDate()) and YEAR(r.DataVencimento) = YEAR(getDate()) ";

                var receitas = cn.Query<Receitas, CategoriaFinanceira, TipoPagamento, SubCategoriaFinanceira, Receitas>(
                    sql,
                    (r, cf, tp, sc) =>
                    {
                        r.CategoriaFinanceiraId = cf.CategoriaFinanceiraId;
                        r.CategoriaFinanceira = cf;
                        r.TipoPagamento = tp;
                        r.SubCategoriaFinanceira = sc;
                        return r;
                    }, splitOn: "ReceitasId, CategoriaFinanceiraId, TipoPagamentoId, SubCategoriaFinanceiraId");

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
                                left join SubCategoriaFinanceira sc on r.SubCategoriaFinanceiraId = sc.SubCategoriaFinanceiraId
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

                var receitas = cn.Query<Receitas, CategoriaFinanceira, TipoPagamento, SubCategoriaFinanceira, Receitas >(
                    sql,
                    (r, cf, tp, sc) =>
                    {
                        r.CategoriaFinanceiraId = cf.CategoriaFinanceiraId;
                        r.CategoriaFinanceira = cf;
                        r.TipoPagamento = tp;
                        r.SubCategoriaFinanceira = sc;
                        return r;
                    }, splitOn: "DespesasId, CategoriaFinanceiraId, TipoPagamentoId, SubCategoriaFinanceiraId");


                cn.Close();
                return receitas;
            }
        }
    }
}