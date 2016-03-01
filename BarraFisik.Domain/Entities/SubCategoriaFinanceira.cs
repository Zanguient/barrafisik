using System;

namespace BarraFisik.Domain.Entities
{
    public class SubCategoriaFinanceira
    {
        public SubCategoriaFinanceira()
        {
            SubCategoriaFinanceiraId = Guid.NewGuid();
        }

        public Guid SubCategoriaFinanceiraId { get; set; }
        public string SubCategoria { get; set; }

        public Guid CategoriaFinanceiraId { get; set; }
        public virtual CategoriaFinanceira CategoriaFinanceira { get; set; }
    }
}