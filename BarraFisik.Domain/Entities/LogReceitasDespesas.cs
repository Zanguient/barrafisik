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
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string Nome { get; set; }
        public string Observacao { get; set; }

        public string CategoriaFinanceiraId { get; set; }

        //ID
        public string RegistroId{ get; set; }

        //Log
        public string Acao { get; set; }
        public DateTime DataAcao { get; set; }
        public string UserId { get; set; }
        public string UsuarioNome { get; set; }
    }
}