using System;
using System.Collections.Generic;

namespace BarraFisik.Domain.Entities
{
    public class Vendas
    {
        public Vendas()
        {
            VendaId = Guid.NewGuid();
        }

        public Guid VendaId { get; set; }
        public Guid? ClienteId { get; set; }
        public Guid? FuncionarioId { get; set; }
        public int? TipoPagamentoId { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataVenda { get; set; }
        public DateTime? DataPagamento { get; set; }
        public Guid ReceitasId { get; set; }
        public string Descricao { get; set; }

        public virtual Funcionarios Funcionarios { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual TipoPagamento TipoPagamento { get; set; }
        public virtual Receitas Receitas { get; set; }
    }
}