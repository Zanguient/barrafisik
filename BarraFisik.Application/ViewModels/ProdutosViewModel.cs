using System;
using System.ComponentModel.DataAnnotations;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Application.ViewModels
{
    public class ProdutosViewModel
    {
        public ProdutosViewModel()
        {
            ProdutoId = Guid.NewGuid();
        }

        [Key]
        public Guid ProdutoId { get; set; }

        [Required(ErrorMessage = "Informe o Produto")]
        [MaxLength(100, ErrorMessage = "Máximo {0} caracteres")]
        public string Nome { get; set; }

        [MaxLength(200, ErrorMessage = "Máximo {0} caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage="Informe a Categoria do Produto")]
        public Guid ProdutoCategoriaId { get; set; }
        public virtual ProdutosCategoria ProdutosCategoria { get; set; }
    }
}
