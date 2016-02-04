using BarraFisik.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace BarraFisik.Application.ViewModels
{
    public class SubCategoriaFinanceiraViewModel
    {
        public SubCategoriaFinanceiraViewModel()
        {
            SubCategoriaFinanceiraId = Guid.NewGuid();
        }

        [Key]
        public Guid SubCategoriaFinanceiraId { get; set; }

        [Required(ErrorMessage = "Informe a SubCategoria")]
        [MaxLength(100, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        public string SubCategoria { get; set; }

        public Guid CategoriaFinanceiraId { get; set; }
        public virtual CategoriaFinanceira CategoriaFinanceira { get; set; }
    }
}