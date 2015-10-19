using System;
using System.ComponentModel.DataAnnotations;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Application.ViewModels
{
    public class ReceitasViewModel
    {
        public ReceitasViewModel()
        {
            ReceitasId = Guid.NewGuid();
        }

        [Key]
        public Guid ReceitasId { get; set; }
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Informe o Valor")]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Máximo de 2 casas decimais")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Informe o Nome da Receita")]
        [MaxLength(100, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        public string Nome { get; set; }


        [MaxLength(250, ErrorMessage = "Máximo {0} caracteres")]
        public string Observacao { get; set; }

        [Required(ErrorMessage = "Informe a Categoria Financeira")]
        public Guid CategoriaFinanceiraId { get; set; }
        public virtual CategoriaFinanceira CategoriaFinanceira { get; set; }
    }
}