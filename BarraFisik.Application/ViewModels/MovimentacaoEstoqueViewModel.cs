using System;
using System.ComponentModel.DataAnnotations;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Application.ViewModels
{
    public class MovimentacaoEstoqueViewModel
    {
        public MovimentacaoEstoqueViewModel()
        {
            MovimentacaoId = Guid.NewGuid();
        }

        [Key]
        public Guid MovimentacaoId { get; set; }

        public Guid? FornecedorId { get; set; }

        [Required(ErrorMessage = "Informe o Armazém")]
        public Guid ArmazemId { get; set; }
        [Required(ErrorMessage = "Informe o Produto")]
        public Guid ProdutoId { get; set; }
        [Required(ErrorMessage = "Informe o Estoque")]
        public Guid EstoqueId { get; set; }
        [Required(ErrorMessage = "Informe a Quantidade")]
        public int Quantidade { get; set; }
        [Required(ErrorMessage = "Informe o Valor Custo")]
        public decimal ValorUnCusto { get; set; }
        [Required(ErrorMessage = "Informe o Valor Venda")]
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotalCusto { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataMovimento { get; set; }

        public string TipoMovimento { get; set; }

        public virtual Produtos Produtos { get; set; }
        public virtual Armazem Armazem { get; set; }
        public virtual Fornecedores Fornecedores { get; set; }
        public virtual Estoque Estoque { get; set; }

    }
}