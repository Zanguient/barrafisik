using System;

namespace BarraFisik.Application.ViewModels
{
    public class RelatorioFinanceiroSearchViewModel
    {
        public string Tipo { get; set; }
        public string CategoriaId { get; set; }
        public string SubCategoriaId { get; set; }
        public string Situacao { get; set; }
        public string Documento { get; set; }

        public DateTime? EmissaoInicio { get; set; }
        public DateTime? EmissaoFim { get; set; }

        public DateTime? VencimentoInicio { get; set; }
        public DateTime? VencimentoFim { get; set; }

        public DateTime? PagamentoInicio { get; set; }
        public DateTime? PagamentoFim { get; set; }

        public string Cliente { get; set; }
        public string FuncionarioId { get; set; }
        public string TipoPagamentoId { get; set; }
        public string FornecedorId{ get; set; }        
    }
}