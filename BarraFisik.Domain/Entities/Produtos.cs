using System;

namespace BarraFisik.Domain.Entities
{
    public class Produtos
    {
        public Produtos()
        {
            ProdutoId = Guid.NewGuid();
        }
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public Guid ProdutoCategoriaId { get; set; }
        public virtual ProdutosCategoria ProdutosCategoria { get; set; }
    }
}