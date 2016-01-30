using System;

namespace BarraFisik.Domain.Entities
{
    public class LogReceitasDespesas
    {
        public LogReceitasDespesas()
        {
            LogReceitasDespesasId = Guid.NewGuid();
        }

        public Guid LogReceitasDespesasId { get; set; }
        public string Documento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataEmissao { get; set; }
        public decimal Valor { get; set; }
        public decimal Juros { get; set; }
        public decimal Multa { get; set; }
        public decimal ValorTotal { get; set; }
        public string Observacao { get; set; }
        public string Situacao { get; set; }

        public string CategoriaFinanceiraId { get; set; }
        public string FornecedorId { get; set; }
        public string FuncionarioId { get; set; }
        public string ClienteId { get; set; }
        public int? TipoPagamentoId { get; set; }

        //ID
        public string RegistroId{ get; set; }

        //Log
        public string Acao { get; set; }
        public DateTime DataAcao { get; set; }
        public string UserId { get; set; }
        public string UsuarioNome { get; set; }
    }
}