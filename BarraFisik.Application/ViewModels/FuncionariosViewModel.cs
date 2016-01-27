using System;
using System.ComponentModel.DataAnnotations;

namespace BarraFisik.Application.ViewModels
{
    public class FuncionariosViewModel
    {
        public FuncionariosViewModel()
        {
            FuncionarioId = Guid.NewGuid();
        }

        [Key]
        public Guid FuncionarioId { get; set; }

        [Required(ErrorMessage = "Informe o Nome do Funcionário")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public bool isAtivo { get; set; }
    }
}
