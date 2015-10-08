using System;
using System.Web;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Services;
using Microsoft.AspNet.Identity;

namespace BarraFisik.Domain.Services
{
    public class LogSistemaService : ServiceBase<LogSistema>, ILogSistemaService
    {
        private readonly ILogSistemaRepository _logSistemaRepository;

        public LogSistemaService(ILogSistemaRepository logSistemaRepository) : base(logSistemaRepository)
        {
            _logSistemaRepository = logSistemaRepository;
        }

        public void AddLog(string tabela, Guid registroId, string acao, string descricao)
        {
            base.Add(GetLog(tabela, registroId, acao, descricao));
        }

        private static LogSistema GetLog(string tabela, Guid registroId, string acao, string descricao)
        {
            var log = new LogSistema()
            {
                Data = DateTime.Now,
                UsuarioNome = HttpContext.Current.User.Identity.Name,
                UserId = HttpContext.Current.User.Identity.GetUserId(),
                Acao = acao,
                RegistroId = registroId,
                Tabela = tabela,
                Descricao = descricao
            };

            return log;
        }
    }

}