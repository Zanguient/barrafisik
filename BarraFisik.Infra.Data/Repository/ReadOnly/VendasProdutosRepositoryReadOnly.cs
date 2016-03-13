using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using Dapper;

namespace BarraFisik.Infra.Data.Repository.ReadOnly
{
    public class VendasProdutosRepositoryReadOnly : RepositoryBaseReadOnly, IVendasProdutosRepositoryReadOnly
    {
        public void DeleteVendaProduto(VendasProdutos produto)
        {
            using (var cn = Connection)
            {
                cn.Open();
                var sql = @"delete from VendasProdutos where VendasProdutosId = '" + produto.VendasProdutosId +"'";
                cn.Query(sql);
                cn.Close();
            }
        }
    }
}