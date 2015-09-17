using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BarraFisik.Application.ViewModels
{
    public class MensalidadesViewModel
    {
        public MensalidadesViewModel()
        {
            MensalidadesId = Guid.NewGuid();
        }

        [Key]
        public Guid MensalidadesId { get; set; }

        [Required(ErrorMessage = "Informe o Mês")]
        public int MesReferencia { get; set; }

        [Required(ErrorMessage = "Informe o Ano")]
        public int AnoReferencia { get; set; }

        [Required(ErrorMessage = "Informe a data do pagamento")]
        public DateTime DataPagamento { get; set; }

        [Required(ErrorMessage = "Favor selecionar cliente")]
        public Guid ClienteId { get; set; }

        [Required(ErrorMessage = "Informar o Valor Pago")]
        public decimal ValorPago { get; set; }

    }
}