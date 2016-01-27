using System;

namespace BarraFisik.Domain.Entities
{
    public class Fornecedores
    {
        public Fornecedores()
        {
            FornecedorId = Guid.NewGuid();
        }

        public Guid FornecedorId { get; set; }
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string Email { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string Celular { get; set; }
        public string Fax { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public bool isAtivo { get; set; }
    }
}
