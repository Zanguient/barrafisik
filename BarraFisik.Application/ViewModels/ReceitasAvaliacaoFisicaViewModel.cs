using System;
using System.ComponentModel.DataAnnotations;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Application.ViewModels
{
    public class ReceitasAvaliacaoFisicaViewModel
    {
        public ReceitasAvaliacaoFisicaViewModel()
        {
            ReceitasAvaliacaoFisicaId = Guid.NewGuid();
        }

        [Key]
        public Guid ReceitasAvaliacaoFisicaId { get; set; }

        [Required(ErrorMessage = "Informe o valor do pagamento")]
        [RegularExpression(@"^\d+.\d{0,2}$")]
        [Range(0, 999.99)]
        public decimal Valor { get; set; }

        public Guid ClienteId { get; set; }
        public DateTime DataPagamento { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}