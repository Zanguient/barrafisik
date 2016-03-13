using System;
using System.Linq;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using Dapper;

namespace BarraFisik.Infra.Data.Repository.ReadOnly
{
    public class EstoqueRepositoryReadOnly : RepositoryBaseReadOnly, IEstoqueRepositoryReadOnly
    {
        public bool ExisteEstoque(Guid armazemId, Guid produtoId)
        {
            using (var cn = Connection)
            {
                var query = @"  SELECT CASE WHEN EXISTS (
                                SELECT *
                                FROM Estoque e 
                                WHERE	e.ArmazemId = '" + armazemId + "' and " +
                            "e.ProdutoId = '" + produtoId + "'" +
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

        public void AtualizaProdutos(Estoque estoque)
        {
            using (var cn = Connection)
            {
                var query = @"  update Estoque set Quantidade = "+estoque.Quantidade+ " , SaldoVenda = Replace('"+ estoque.SaldoVenda + "', ',', '.'), TotalVendido = "+estoque.TotalVendido+" where EstoqueId = '"+estoque.EstoqueId+"'";

                cn.Open();
                var valido = cn.Query(query);
                cn.Close();               
            }
        }
    }
}