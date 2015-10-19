using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BarraFisik.Application.ViewModels
{
    public class CategoriaFinanceiraViewModel
    {
        public CategoriaFinanceiraViewModel()
        {
            CategoriaFinanceiraId = Guid.NewGuid();
        }

        [Key]
        public Guid CategoriaFinanceiraId { get; set; }

        [Required(ErrorMessage = "Informe o Tipo")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "Informe a Categoria")]
        [MaxLength(100, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        public string Categoria { get; set; }

    }
}