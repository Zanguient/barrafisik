using System;
using System.ComponentModel.DataAnnotations;

namespace BarraFisik.Application.ViewModels
{
    public class ArmazemViewModel
    {
        public ArmazemViewModel()
        {
            ArmazemId = Guid.NewGuid();
        }

        [Key]
        public Guid ArmazemId { get; set; }

        [Required(ErrorMessage = "Informe a Descrição")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        public string Descricao { get; set; }
    }
}
