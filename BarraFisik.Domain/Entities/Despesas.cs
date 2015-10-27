using System;

namespace BarraFisik.Domain.Entities
{
    public class Despesas
    {
        public Despesas()
        {
            DespesasId = Guid.NewGuid();
        }

        public Guid DespesasId { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string Nome { get; set; }
        public string Observacao { get; set; }

        public Guid CategoriaFinanceiraId { get; set; }
        public virtual CategoriaFinanceira CategoriaFinanceira { get; set; }

        public string DataDespesa { get; set; }
    }
}