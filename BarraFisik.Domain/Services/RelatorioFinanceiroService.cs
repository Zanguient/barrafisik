using System;
using System.Collections.Generic;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Services
{
    public class RelatorioFinanceiroService : ServiceBase<RelatorioFinanceiro>, IRelatorioFinanceiroService
    {
        private readonly IRelatorioFinanceiroRepositoryReadOnly _relatorioFinanceiroRepositoryReadOnly;
        private readonly IRelatorioFinanceiroRepository _relatorioFinanceiroRepository;

        public RelatorioFinanceiroService(IRelatorioFinanceiroRepositoryReadOnly relatorioFinanceiroRepositoryReadOnly, IRelatorioFinanceiroRepository relatorioFinanceiroRepository)
            : base(relatorioFinanceiroRepository)
        {
            _relatorioFinanceiroRepositoryReadOnly = relatorioFinanceiroRepositoryReadOnly;
            _relatorioFinanceiroRepository = relatorioFinanceiroRepository;
        }


        public IEnumerable<RelatorioFinanceiro> GetRelatorio(RelatorioFinanceiroSearch filters)
        {
            return _relatorioFinanceiroRepositoryReadOnly.GetRelatorio(filters);
        }

        public IEnumerable<RelatorioFinanceiro> GetRelatorioReceitas(RelatorioFinanceiroSearch filters)
        {
            return _relatorioFinanceiroRepositoryReadOnly.GetRelatorioReceitas(filters);
        }

        public IEnumerable<RelatorioFinanceiro> GetRelatorioDespesas(RelatorioFinanceiroSearch filters)
        {
            return _relatorioFinanceiroRepositoryReadOnly.GetRelatorioDespesas(filters);
        }

        public IEnumerable<RelatorioFinanceiroTotalMeses> GetTotalPorMes()
        {
            return _relatorioFinanceiroRepositoryReadOnly.GetTotalPorMes();
        }
    }
}