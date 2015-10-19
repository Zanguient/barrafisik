using System;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface ILogReceitasDespesasService : IServiceBase<LogReceitasDespesas>
    {
        void AddLog(string acao, LogReceitasDespesas logReceitasDespesas);
    }
}