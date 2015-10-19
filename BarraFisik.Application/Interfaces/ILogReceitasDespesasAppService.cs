using System;
using BarraFisik.Domain.Entities;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Application.Interfaces
{
    public interface ILogReceitasDespesasAppService : IAppServiceBase<BarraFisikContext>
    {
        void AddLog(string acao, Guid registroId, LogReceitasDespesas logReceitasDespesas);
    }
}