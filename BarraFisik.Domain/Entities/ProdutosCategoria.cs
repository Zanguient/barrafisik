using System;

namespace BarraFisik.Domain.Entities
{
    public class ProdutosCategoria
    {
        public ProdutosCategoria()
        {
            ProdutoCategoriaId = Guid.NewGuid();
        }
        public Guid ProdutoCategoriaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}