using System;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface ILogMensalidadesService : IServiceBase<LogMensalidades>
    {
        void AddLog(string acao, Mensalidades mensalidade);
    }
}