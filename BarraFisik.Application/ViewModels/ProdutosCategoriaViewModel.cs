using System;
using System.ComponentModel.DataAnnotations;

namespace BarraFisik.Application.ViewModels
{
    public class ProdutosCategoriaViewModel
    {
        public ProdutosCategoriaViewModel()
        {
            ProdutoCategoriaId = Guid.NewGuid();
        }

        [Key]
        public Guid ProdutoCategoriaId { get; set; }

        [Required(ErrorMessage = "Informe o Produto")]
        [MaxLength(100, ErrorMessage = "Máximo {0} caracteres")]
        public string Nome { get; set; }

        [MaxLength(200, ErrorMessage = "Máximo {0} caracteres")]
        public string Descricao { get; set; }
    }
}
