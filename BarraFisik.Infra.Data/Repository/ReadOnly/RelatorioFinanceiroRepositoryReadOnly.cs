using System.Collections.Generic;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.ValueObjects;
using Dapper;

namespace BarraFisik.Infra.Data.Repository.ReadOnly
{
    public class RelatorioFinanceiroRepositoryReadOnly : RepositoryBaseReadOnly, IRelatorioFinanceiroRepositoryReadOnly
    {
        public IEnumerable<RelatorioFinanceiro> GetRelatorio(RelatorioFinanceiroSearch filters)
        {
            using (var cn = Connection)
            {
                var query = @"select 
	                            d.Data,
	                            d.Nome as Nome, 
	                            d.Observacao as Observacao, 
	                            d.Valor as Valor, 
	                            cf.Categoria as Categoria,
	                            d.DespesasId as RegistroId,
	                            cf.Tipo  as Tipo
                            from Despesas d, CategoriaFinanceira cf 
                            where d.CategoriaFinanceiraId = cf.CategoriaFinanceiraId ";

                            if (filters.Tipo != null && filters.Tipo != "Todos")
                                query = query + " and cf.Tipo = '"+ filters.Tipo+ "' ";
                            if (filters.DataInicio != null)
                                query = query + " and d.Data >= '" + filters.DataInicio +"' ";
                            if (filters.DataFim != null)
                                query = query + " and d.Data <= '" + filters.DataFim + "' ";
                            if (filters.Categoria != null)
                                query = query + " and cf.Categoria = '" + filters.Categoria + "' ";
                            if (filters.Nome != null)
                                query = query + " and d.Nome like '%" + filters.Nome + "%' ";

                query = query +
                        " union" +
                        " select " +
                        "    r.Data as Data, " +
                        "    r.Nome as Nome, " +
                        "    r.Observacao as Observacao, " +
                        "    r.Valor as Valor, " +
                        "    cf.Categoria as Categoria," +
                        "    r.ReceitasId as RegistroId," +
                        "    cf.Tipo  as Tipo" +
                        " from Receitas r, CategoriaFinanceira cf " +
                        " where r.CategoriaFinanceiraId = cf.CategoriaFinanceiraId";

                if (filters.Tipo != null && filters.Tipo != "Todos")
                    query = query + " and cf.Tipo = '" + filters.Tipo + "' ";
                if (filters.DataInicio != null)
                    query = query + " and r.Data >= '" + filters.DataInicio + "' ";
                if (filters.DataFim != null)
                    query = query + " and r.Data <= '" + filters.DataFim + "' ";
                if(filters.Categoria != null)
                    query = query + " and cf.Categoria = '" + filters.Categoria + "' ";
                if(filters.Nome != null)
                    query = query + " and r.Nome like '%" + filters.Nome + "%' ";

                query = query +
                        " union" +
                        " select  " +
                        "    m.DataPagamento as Data," +
                        "    m.Nome as Nome, " +
                        "    '' as Observacao, " +
                        "    m.ValorPago as Valor, " +
                        "    cf.Categoria as Categoria," +
                        "    m.MensalidadesId as RegistroId," +
                        "    cf.Tipo  as Tipo" +
                        " from Mensalidades m, CategoriaFinanceira cf " +
                        " where m.CategoriaFinanceiraId = cf.CategoriaFinanceiraId";

                if (filters.Tipo != null && filters.Tipo != "Todos")
                    query = query + " and cf.Tipo = '" + filters.Tipo + "' ";
                if (filters.DataInicio != null)
                    query = query + " and m.DataPagamento >= '" + filters.DataInicio + "' ";
                if (filters.DataFim != null)
                    query = query + " and m.DataPagamento <= '" + filters.DataFim + "' ";
                if (filters.Categoria != null)
                    query = query + " and cf.Categoria = '" + filters.Categoria + "' ";
                if (filters.Nome != null)
                    query = query + " and m.Nome like '%" + filters.Nome + "%' ";


                cn.Open();
                var relatorio = cn.Query<RelatorioFinanceiro>(query);
                cn.Close();

                return relatorio;
            }
        }
    }
}