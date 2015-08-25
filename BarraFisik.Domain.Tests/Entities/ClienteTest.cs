using System.Linq;
using BarraFisik.Domain.Entities;
using NUnit.Framework;

namespace BarraFisik.Domain.Tests.Entities
{
    [TestFixture]
    class ClienteTest
    {
        private Cliente _cliente;

        [Test]
        public void Nao_Deve_Aceitar_Cliente_Com_Cpf_Invalido()
        {
            _cliente = new Cliente()
            {
                Nome = "Jefferson Ribeiro Shibuya",
                Endereco = "Rua Teste",
                Cpf = "32152147895"
            };

            Assert.IsFalse(_cliente.IsValid());
            Assert.Contains("CPF informado é inválido", _cliente.ResultadoValidacao.Erros.Select(error => error.Message).ToList());
        }
    }
}