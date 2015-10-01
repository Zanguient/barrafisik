using System;
using System.ComponentModel;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.ValueObjects
{
    public class ClienteHorario
    {
        public ClienteHorario()
        {
            ClienteId = Guid.NewGuid();
            HorarioId = Guid.NewGuid();            
        }

        //CLiente
        public Guid ClienteId { get; set; }

        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public DateTime DtNascimento { get; set; }
        public DateTime DtInscricao { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Sexo { get; set; }
        public int QtdFilhos { get; set; }
        public bool IsAtivo { get; set; }
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