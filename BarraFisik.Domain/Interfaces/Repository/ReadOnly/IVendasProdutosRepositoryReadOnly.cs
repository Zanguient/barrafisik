using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Repository.ReadOnly
{
    public interface IVendasProdutosRepositoryReadOnly
    {
        void DeleteVendaProduto(VendasProdutos produto);
    }
}