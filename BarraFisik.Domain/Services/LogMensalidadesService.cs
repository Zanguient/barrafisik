using System;
using System.Web;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Services;
using Microsoft.AspNet.Identity;

namespace BarraFisik.Domain.Services
{
    public class LogMensalidadesService : ServiceBase<LogMensalidades>, ILogMensalidadesService
    {
        private readonly ILogMensalidadesRepository _logMensalidadesRepository;

        public LogMensalidadesService(ILogMensalidadesRepository logMensalidadesRepository) : base(logMensalidadesRepository)
        {
            _logMensalidadesRepository = logMensalidadesRepository;
        }

        public void AddLog(string acao, Mensalidades mensalidade)
        {
            base.Add(GetLog(acao, mensalidade));
        }

        private static LogMensalidades GetLog(string acao, Mensalidades mensalidade)
        {
            var logMensalidade = new LogMensalidades();
            {
                logMensalidade.Data = DateTime.Now;
                logMensalidade.UsuarioNome = HttpContext.Current.User.Identity.Name;
                logMensalidade.UserId = HttpContext.Current.User.Identity.GetUserId();
                logMensalidade.Acao = acao;

                logMensalidade.MensalidadesId = mensalidade.MensalidadesId;
                logMensalidade.ClienteId = mensalidade.ClienteId;
                logMensalidade.MesReferencia = mensalidade.MesReferencia; 
                logMensalidade.AnoReferencia = mensalidade.AnoReferencia; 
                logMensalidade.ValorPago = mensalidade.ValorPago; 
                logMensalidade.DataPagamento = mensalidade.DataPagamento;
                logMensalidade.isPersonal = mensalidade.isPersonal;
                logMensalidade.ValorPersonal = mensalidade.ValorPersonal;
            };

            return logMensalidade;
        }
    }

}