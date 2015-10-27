using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using Dapper;

namespace BarraFisik.Infra.Data.Repository.ReadOnly
{
    public class DespesasRepositoryReadOnly : RepositoryBaseReadOnly, IDespesasRepositoryReadOnly
    {
        public IEnumerable<Despesas> GetDespesas()
        {
            using (var cn = Connection)
            {
                cn.Open();

                var sql = @"select  *, CONVERT(varchar(10), d.Data, 103) as DataDespesa
                                from Despesas d
                                inner join CategoriaFinanceira cf on d.CategoriaFinanceiraId = cf.CategoriaFinanceiraId";

                var despesas = cn.Query<Despesas, CategoriaFinanceira, string, Despesas>(
                    sql,
                    (d, cf, DataDespesa) =>
                    {
                        d.CategoriaFinanceiraId = cf.CategoriaFinanceiraId;
                        d.CategoriaFinanceira = cf;
                        d.DataDespesa = DataDespesa;
                        return d;

                    }, splitOn: "DespesasId, CategoriaFinanceiraId, DataDespesa");


                cn.Close();
                return despesas;             
            }
        }
    }
}