using System;
using System.ComponentModel.DataAnnotations;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Application.ViewModels
{
    public class EstoqueViewModel
    {
        public EstoqueViewModel()
        {
            EstoqueId = Guid.NewGuid();
        }

        [Key]
        public Guid EstoqueId { get; set; }

        [Required(ErrorMessage = "Informe o Armazém")]
        public Guid ArmazemId { get; set; }

        [Required(ErrorMessage = "Informe o Produto")]
        public Guid ProdutoId { get; set; }

        [Required(ErrorMessage = "Informe a Quantidade")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Informe o Valor Unitário")]
        public decimal ValorUnitario { get; set; }

        public decimal ValorTotal { get; set; }
        public decimal SaldoVenda { get; set; }
        public int TotalVendido { get; set; }

        public virtual Armazem Armazem{ get; set; }
        public virtual Produtos Produtos{ get; set; }
    }
}