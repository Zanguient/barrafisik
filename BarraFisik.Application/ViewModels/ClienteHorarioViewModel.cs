using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BarraFisik.Application.ViewModels
{
    public class ClienteHorarioViewModel
    {
        public ClienteHorarioViewModel()
        {
            ClienteId = Guid.NewGuid();
            HorarioId = Guid.NewGuid();
            //ValoresId = Guid.NewGuid();
        }

        //CLiente
        public Guid ClienteId { get; set; }

        [Required(ErrorMessage = "Informe o Nome")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o Endereço")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        public string Endereco { get; set; }

        public string Telefone { get; set; }
        public string Celular { get; set; }

        [Required(ErrorMessage = "Informe a Data de Nascimento")]
        [DisplayName("Data de Nascimento")]
        public DateTime DtNascimento { get; set; }

        [Required(ErrorMessage = "Informe a Data de Inscrição")]
        public DateTime DtInscricao { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo {0} caracteres")]
        [EmailAddress(ErrorMessage = "Preencha um E-mail válido")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o CPF")]
        [MaxLength(11, ErrorMessage = "Máximo {0} caracteres")]
        public string Cpf { get; set; }

        public string Rg { get; set; }

        public string Sexo { get; set; }
        public int QtdFilhos { get; set; }

        [DisplayName("Ativo")]
        public bool IsAtivo { get; set; }

        public byte[] Foto { get; set; }

        public string Path { get; set; }

        public string Situacao { get; set; }

        //Horario Cliente
        public Guid HorarioId { get; set; }

        public bool Segunda { get; set; }
        public string HSegunda { get; set; }

        public bool Terca { get; set; }
        public string HTerca { get; set; }

        public bool Quarta { get; set; }
        public string HQuarta { get; set; }

        public bool Quinta { get; set; }
        public string HQuinta { get; set; }

        public bool Sexta { get; set; }
        public string HSexta { get; set; }

        //Valor Mensalidade
        public Guid? ValoresId { get; set; }
        public int QtdDias { get; set; }
        public decimal Valor { get; set; }
        public int HorarioInicio { get; set; }
        public int HorarioFim { get; set; }
    }
}