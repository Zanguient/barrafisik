using System.Collections.Generic;
using System.Data;
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
                        "    c.Nome as Observacao, " +
                        "    m.ValorPago as Valor, " +
                        "    cf.Categoria as Categoria," +
                        "    m.MensalidadesId as RegistroId," +
                        "    cf.Tipo  as Tipo" +
                        " from Mensalidades m, CategoriaFinanceira cf, Cliente c " +                        
                        " where m.CategoriaFinanceiraId = cf.CategoriaFinanceiraId and m.ClienteId = c.ClienteId ";

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

                query = query +
                        "union " +
                        " select  " +
                        "    af.DataPagamento as Data," +
                        "    af.Nome as Nome, " +
                        "    c.Nome as Observacao, " +
                        "    af.Valor as Valor, " +
                        "    cf.Categoria as Categoria," +
                        "    af.ReceitasAvaliacaoFisicaId as RegistroId," +
                        "    cf.Tipo  as Tipo" +
                        " from ReceitasAvaliacoesFisicas af, CategoriaFinanceira cf, Cliente c " +
                        " where af.CategoriaFinanceiraId = cf.CategoriaFinanceiraId and af.ClienteId = c.ClienteId ";

                if (filters.Tipo != null && filters.Tipo != "Todos")
                    query = query + " and cf.Tipo = '" + filters.Tipo + "' ";
                if (filters.DataInicio != null)
                    query = query + " and af.DataPagamento >= '" + filters.DataInicio + "' ";
                if (filters.DataFim != null)
                    query = query + " and af.DataPagamento <= '" + filters.DataFim + "' ";
                if (filters.Categoria != null)
                    query = query + " and cf.Categoria = '" + filters.Categoria + "' ";
                if (filters.Nome != null)
                    query = query + " and af.Nome like '%" + filters.Nome + "%' ";
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