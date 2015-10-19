using System;
using System.Web;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Services;
using Microsoft.AspNet.Identity;

namespace BarraFisik.Domain.Services
{
    public class LogReceitasDespesasService : ServiceBase<LogReceitasDespesas>, ILogReceitasDespesasService
    {
        private readonly ILogReceitasDespesasRepository _logReceitasDespesasRepository;

        public LogReceitasDespesasService(ILogReceitasDespesasRepository logReceitasDespesasRepository):base(logReceitasDespesasRepository)
        {
            _logReceitasDespesasRepository = logReceitasDespesasRepository;
        }

        public void AddLog(string acao, LogReceitasDespesas logReceitasDespesas)
        {
            _logReceitasDespesasRepository.Add(GetLog(acao, logReceitasDespesas));
        }

        private static LogReceitasDespesas GetLog(string acao, LogReceitasDespesas logReceitasDespesas)
        {
            logReceitasDespesas.Acao = acao;
            logReceitasDespesas.DataAcao = DateTime.Now;
            logReceitasDespesas.UsuarioNome = HttpContext.Current.User.Identity.Name;
            logReceitasDespesas.UserId = HttpContext.Current.User.Identity.GetUserId();

            return logReceitasDespesas;
        }
    }
}