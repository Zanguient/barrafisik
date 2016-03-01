using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Services;

namespace BarraFisik.Domain.Services
{
    public class ProdutosCategoriaService : ServiceBase<ProdutosCategoria>, IProdutosCategoriaService
    {
        private readonly IProdutosCategoriaRepository _produtosCategoriaRepository;

        public ProdutosCategoriaService(IProdutosCategoriaRepository produtosCategoriaRepository):base(produtosCategoriaRepository)
        {
            _produtosCategoriaRepository = produtosCategoriaRepository;
        }
    }
}
