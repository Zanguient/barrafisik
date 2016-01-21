using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarraFisik.Application.ViewModels
{
    public class TipoPagamentoViewModel
    {
        [Key]
        public int TipoPagamentoId { get; set; }

        [Required(ErrorMessage = "Informe a Sigla")]
        [MaxLength(5, ErrorMessage = "Máximo {0} caracteres")]
        public string Sigla { get; set; }

        [Required(ErrorMessage = "Informe a Descrição")]
        [MaxLength(50, ErrorMessage = "Máximo {0} caracteres")]
        public string Descricao { get; set; }
    }
}
