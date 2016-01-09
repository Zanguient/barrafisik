using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
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

                var sql = @"select 
	                            CONVERT(varchar(10), r.Data, 103) as DataReceita, 
	                            r.Data as Data,
	                            r.Nome as Nome, 
	                            r.Observacao as Observacao, 
	                            r.Valor as Valor, 
	                            cf.Categoria as Categoria,
	                            r.ReceitasId as ReceitasId,
	                            '' as Cliente,
	                            cf.CategoriaFinanceiraId as CategoriaFinanceiraId
                            from Receitas r
                            inner join CategoriaFinanceira cf on r.CategoriaFinanceiraId = cf.CategoriaFinanceiraId
                            union
                            select 
	                            CONVERT(varchar(10), af.DataPagamento, 103) as DataReceita, 
	                            af.DataPagamento as Data,
	                            af.Nome as Nome, 
	                            '' as Observacao, 
	                            af.Valor as Valor, 
	                            cf.Categoria as Categoria,
	                            af.ReceitasAvaliacaoFisicaId as ReceitasId,
	                            af.Nome as Cliente,
	                            cf.CategoriaFinanceiraId as CategoriaFinanceiraId
                            from ReceitasAvaliacoesFisicas af
                            inner join CategoriaFinanceira cf on af.CategoriaFinanceiraId = cf.CategoriaFinanceiraId
                            union
                            select 
	                            CONVERT(varchar(10), m.DataPagamento, 103) as DataReceita, 
	                            m.DataPagamento as Data,
	                            m.Nome as Nome, 
	                            '' as Observacao, 
	                            m.ValorPago as Valor, 
	                            cf.Categoria as Categoria,
	                            m.MensalidadesId as MensalidadesId,
	                            c.Nome as Cliente,
	                            cf.CategoriaFinanceiraId as CategoriaFinanceiraId
                            from Mensalidades m
                            inner join CategoriaFinanceira cf on m.CategoriaFinanceiraId = cf.CategoriaFinanceiraId
                            left join Cliente c on m.ClienteId = c.ClienteId";

                var receitas = cn.Query<Receitas>(sql);

                cn.Close();
                return receitas;
            }
        }
    }
}