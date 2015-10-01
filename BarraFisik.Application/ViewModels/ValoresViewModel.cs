using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BarraFisik.Application.ViewModels
{
    public class ValoresViewModel
    {
        public ValoresViewModel()
        {
            ValoresId = Guid.NewGuid();
        }

        [Key]
        public Guid ValoresId { get; set; }

        [Required(ErrorMessage = "Informe a Qtd. de Dias")]
        [Range(1, 5, ErrorMessage = "A Qtde de dias deve ser entre 1 e 5.")]
        public int QtdDias { get; set; }

        [Required(ErrorMessage = "Informe o valor")]
        [RegularExpression(@"^\d+.\d{0,2}$")]
        [Range(0, 999.99)]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Informe o Horário Início")]
        [Range(6, 20, ErrorMessage = "O Horário deve ser entre 6h - 20h.")]
        public int HorarioInicio { get; set; }

        [Required(ErrorMessage = "Informe o Horário Fim")]
        [Range(6, 20, ErrorMessage = "O Horário deve ser entre 6h - 20h.")]
        public int HorarioFim { get; set; }     

    }
}