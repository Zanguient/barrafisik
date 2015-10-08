using System;

namespace BarraFisik.Domain.Entities
{
    public class LogSistema
    {
        public LogSistema()
        {
            LogSistemaId = Guid.NewGuid();
        }

        public Guid LogSistemaId { get; set; }        
        public DateTime Data { get; set; }
        public string UserId { get; set; }
        public string UsuarioNome { get; set; }
        public string Acao { get; set; }
        public string Tabela { get; set; }
        public Guid RegistroId { get; set; }
        public string Descricao { get; set; }
    }
}