using System;
using System.ComponentModel.DataAnnotations;

namespace BarraFisik.Application.ViewModels
{
    public class FornecedoresViewModel
    {
        public FornecedoresViewModel()
        {
            FornecedorId = Guid.NewGuid();
        }

        [Key]
        public Guid FornecedorId { get; set; }

        [Required(ErrorMessage = "Informe o Nome do Fornecedor")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
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
