using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.ValueObjects;
using Dapper;
using System.Linq;

namespace BarraFisik.Infra.Data.Repository.ReadOnly
{
    public class VendasRepositoryReadOnly : RepositoryBaseReadOnly, IVendasRepositoryReadOnly
    {

        public void DeleteVenda(Vendas venda)
        {
            using (var cn = Connection)
            {
                cn.Open();
                var sql = @"delete from Vendas where VendaId = '" + venda.VendaId + "'";
                cn.Query(sql);
                cn.Close();
            }
        }

        public IEnumerable<Vendas> SearchVendas(SearchVendas sv)
        {
            using (var cn = Connection)
            {
                cn.Open();
                bool hasData = false;
                var sql = @"select * from Vendas v 
                                inner join Receitas r on v.ReceitasId = r.ReceitasId
                                left join Cliente cliente on v.ClienteId = cliente.ClienteId
                                left join TipoPagamento tp on v.TipoPagamentoId = tp.TipoPagamentoId
                                where 1 = 1";

                var dt = new DateTime();

                if (sv.VendaInicio != dt)
                {
                    sql = sql + " AND v.DataVenda >= '" + sv.VendaInicio.ToString("yyyy-MM-dd 00:00:00") + "'";
                    hasData = true;
                }

                if (sv.VendaFim != dt)
                {
                    sql = sql + " AND v.DataVenda <= '" + sv.VendaFim.ToString("yyyy-MM-dd 23:59:59") + "'";
                    hasData = true;
                }


                if (sv.PagamentoInicio != dt)
                {
                    sql = sql + " AND v.DataPagamento >= '" + sv.PagamentoInicio.ToString("yyyy-MM-dd 00:00:00") + "'";
                    hasData = true;
                }

                if (sv.PagamentoFim != dt)
                {
                    sql = sql + " AND v.DataPagamento <= '" + sv.PagamentoFim.ToString("yyyy-MM-dd 23:59:59") + "'";
                    hasData = true;
                }


                if (sv.VencimentoInicio != dt)
                {
                    sql = sql + " AND v.DataVencimento >= '" + sv.VencimentoInicio.ToString("yyyy-MM-dd 00:00:00") + "'";
                    hasData = true;
                }

                if (sv.VencimentoFim != dt)
                {
                    sql = sql + " AND v.DataVencimento <= '" + sv.VencimentoFim.ToString("yyyy-MM-dd 23:59:59") + "'";
                    hasData = true;
                }

                if (!hasData)
                    sql = sql + " AND Month(v.DataVenda) = Month(GetDate()) and YEAR(v.DataVenda) = YEAR(getDate())";


                var vendas = cn.Query<Vendas, Receitas, Cliente, TipoPagamento, Vendas>
                    (sql,
                    (v, r, c, tp) =>
                    {
                        v.Receitas = r;
                        v.Cliente = c;
                        v.TipoPagamento = tp;
                        return v;
                    }, splitOn: "VendaId, ReceitasId, ClienteId, TipoPagamentoId");

                cn.Close();
                return vendas;
            }
        }

        public List<int> GetVendasAnual(int ano)
        {
            using (var cn = Connection)
            {
                //var query = @"  Select distinct " +
                //                    "(select count(*) from Vendas v where Month(v.DataVenda) = 1 and  YEAR(v.DataVenda) =  " + ano + ")  as Jan,   " +
                //                    "(select count(*) from Vendas v where Month(v.DataVenda) = 2 and  YEAR(v.DataVenda) =  " + ano + ")  as Fev,   " +
                //                    "(select count(*) from Vendas v where Month(v.DataVenda) = 3 and  YEAR(v.DataVenda) =  " + ano + ")  as Mar,   " +
                //                    "(select count(*) from Vendas v where Month(v.DataVenda) = 4 and  YEAR(v.DataVenda) =  " + ano + ")  as Abr,   " +
                //                    "(select count(*) from Vendas v where Month(v.DataVenda) = 5 and  YEAR(v.DataVenda) =  " + ano + ")  as Mai,   " +
                //                    "(select count(*) from Vendas v where Month(v.DataVenda) = 6 and  YEAR(v.DataVenda) =  " + ano + ")  as Jun,   " +
                //                    "(select count(*) from Vendas v where Month(v.DataVenda) = 7 and  YEAR(v.DataVenda) =  " + ano + ")  as Jul,   " +
                //                    "(select count(*) from Vendas v where Month(v.DataVenda) = 8 and  YEAR(v.DataVenda) =  " + ano + ")  as Ago,   " +
                //                    "(select count(*) from Vendas v where Month(v.DataVenda) = 9 and  YEAR(v.DataVenda) =  " + ano + ")  as Setemb,   " +
                //                    "(select count(*) from Vendas v where Month(v.DataVenda) = 10 and YEAR(v.DataVenda) =  " + ano + ")   as Outub,   " +
                //                    "(select count(*) from Vendas v where Month(v.DataVenda) = 11 and YEAR(v.DataVenda) =  " + ano + ")   as Nov,   " +
                //                    "(select count(*) from Vendas v where Month(v.DataVenda) = 12 and YEAR(v.DataVenda) =  " + ano + ")   as Dez    " +
                //                " from Vendas v";

                var query = @"Select distinct" +
                                 "(select coalesce(sum(vp.Quantidade),0) from VendasProdutos vp, Vendas v where Month(v.DataVenda) = 1  and  YEAR(v.DataVenda) = " + ano + ") as Jan, " +
                                 "(select coalesce(sum(vp.Quantidade),0) from VendasProdutos vp, Vendas v where Month(v.DataVenda) = 2  and  YEAR(v.DataVenda) = " + ano + ") as Fev, " +
                                 "(select coalesce(sum(vp.Quantidade),0) from VendasProdutos vp, Vendas v where Month(v.DataVenda) = 3  and  YEAR(v.DataVenda) = " + ano + ") as Mar, " +
                                 "(select coalesce(sum(vp.Quantidade),0) from VendasProdutos vp, Vendas v where Month(v.DataVenda) = 4  and  YEAR(v.DataVenda) = " + ano + ") as Abr, " +
                                 "(select coalesce(sum(vp.Quantidade),0) from VendasProdutos vp, Vendas v where Month(v.DataVenda) = 5  and  YEAR(v.DataVenda) = " + ano + ") as Mai, " +
                                 "(select coalesce(sum(vp.Quantidade),0) from VendasProdutos vp, Vendas v where Month(v.DataVenda) = 6  and  YEAR(v.DataVenda) = " + ano + ") as Jun, " +
                                 "(select coalesce(sum(vp.Quantidade),0) from VendasProdutos vp, Vendas v where Month(v.DataVenda) = 7  and  YEAR(v.DataVenda) = " + ano + ") as Jul, " +
                                 "(select coalesce(sum(vp.Quantidade),0) from VendasProdutos vp, Vendas v where Month(v.DataVenda) = 8  and  YEAR(v.DataVenda) = " + ano + ") as Ago, " +
                                 "(select coalesce(sum(vp.Quantidade),0) from VendasProdutos vp, Vendas v where Month(v.DataVenda) = 9  and  YEAR(v.DataVenda) = " + ano + ") as Setemb, " +
                                 "(select coalesce(sum(vp.Quantidade),0) from VendasProdutos vp, Vendas v where Month(v.DataVenda) = 10 and  YEAR(v.DataVenda) = " + ano + ") as Outub, " +
                                 "(select coalesce(sum(vp.Quantidade),0) from VendasProdutos vp, Vendas v where Month(v.DataVenda) = 11 and  YEAR(v.DataVenda) = " + ano + ") as Nov, " +
                                 "(select coalesce(sum(vp.Quantidade),0) from VendasProdutos vp, Vendas v where Month(v.DataVenda) = 12 and  YEAR(v.DataVenda) = " + ano + ") as Dez ";
                cn.Open();

                List<int> x = new List<int>();
                foreach (var item in cn.Query(query))
                {
                    x.Add(item.Jan);
                    x.Add(item.Fev);                    
                    x.Add(item.Mar);                    
                    x.Add(item.Abr);                    
                    x.Add(item.Mai);                    
                    x.Add(item.Jun);                    
                    x.Add(item.Jul);                    
                    x.Add(item.Ago);                    
                    x.Add(item.Setemb);                    
                    x.Add(item.Outub);                    
                    x.Add(item.Nov);                    
                    x.Add(item.Dez);                    
                }

                cn.Close();

                return x;
            }
        }

        
    }
}