using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BarraFisik.Application.ViewModels
{
    public class FilaEsperaViewModel
    {
        public FilaEsperaViewModel()
        {
            FilaEsperaId = Guid.NewGuid();
        }

        [Key]
        public Guid FilaEsperaId { get; set; }

        [Required(ErrorMessage = "Informe o Nome")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        public string Nome { get; set; }

        public string Telefone { get; set; }
        public string Celular { get; set; }
        public DateTime DataReserva { get; set; }
        public int? Hora { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo {0} caracteres")]
        [EmailAddress(ErrorMessage = "Preencha um E-mail válido")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

    }
}