using System;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface ILogSistemaService : IServiceBase<LogSistema>
    {
        void AddLog(string tabela, Guid registroId, string acao, string descricao);
    }
}