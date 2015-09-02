using System;
using BarraFisik.Domain.Interfaces.Validation;
using BarraFisik.Domain.Validation.Clientes;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Entities
{
    public class Cliente : ISelfValidation
    {
        public Cliente()
        {
            ClienteId = Guid.NewGuid();
        }

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

        public ValidationResult ResultadoValidacao { get; private set; }

        public bool IsValid()
        {
            var fiscal = new ClienteEstaAptoParaCadastroNoSistema();

            ResultadoValidacao = fiscal.Validar(this);

            return ResultadoValidacao.IsValid;
        }

        
    }
}
