using System;

namespace BarraFisik.Domain.Entities
{
    public class MovimentacaoEstoque
    {
        public MovimentacaoEstoque()
        {
            MovimentacaoId = Guid.NewGuid();
        }

        public Guid MovimentacaoId { get; set; }
        public Guid? FornecedorId { get; set; }
        public Guid ArmazemId { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnCusto { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotalCusto { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataMovimento { get; set; }
        public string TipoMovimento { get; set; }

        public Guid EstoqueId { get; set; }

        public virtual Produtos Produtos { get; set; }
        public virtual Armazem Armazem { get; set; }
        public virtual Fornecedores Fornecedores { get; set; }
        public virtual Estoque Estoque { get; set; }

    }
}