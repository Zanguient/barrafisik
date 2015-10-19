using System;

namespace BarraFisik.Domain.Entities
{
    public class CategoriaFinanceira
    {
        public CategoriaFinanceira()
        {
            CategoriaFinanceiraId = Guid.NewGuid();
        }

        public Guid CategoriaFinanceiraId { get; set; }
        public string Tipo { get; set; }
        public string Categoria { get; set; }
    }
}