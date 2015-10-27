using System;

namespace BarraFisik.Domain.Entities
{
    public class Receitas
    {
        public Receitas()
        {
            ReceitasId = Guid.NewGuid();
        }

        public Guid ReceitasId { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string Nome { get; set; }
        public string Observacao { get; set; }

        public Guid CategoriaFinanceiraId { get; set; }
        public virtual CategoriaFinanceira CategoriaFinanceira { get; set; }

        public string DataReceita { get; set; }
        public virtual string Cliente { get; set; }
        public virtual string Categoria { get; set; }
    }
}