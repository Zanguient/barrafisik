using System;
using System.ComponentModel.DataAnnotations;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Application.ViewModels
{
    public class VendasProdutosViewModel
    {
        public VendasProdutosViewModel()
        {
            VendasProdutosId = Guid.NewGuid();
        }

        [Key]
        public Guid VendasProdutosId { get; set; }

        public Guid VendaId { get; set; }
        public Guid EstoqueId { get; set; }

        public int Quantidade { get; set; }

        public virtual Vendas Vendas { get; set; }
        public virtual Estoque Estoque { get; set; }
    }
}
