using BarraFisik.Application.ViewModels;
using BarraFisik.Infra.Data.Context;
using System;
using System.Collections.Generic;

namespace BarraFisik.Application.Interfaces
{
    public interface ITipoPagamentoAppService : IAppServiceBase<BarraFisikContext>
    {
        void Add(TipoPagamentoViewModel tipoPagamentoViewModel);
        TipoPagamentoViewModel GetById(int id);
        IEnumerable<TipoPagamentoViewModel> GetAll();
        void Update(TipoPagamentoViewModel tipoPagamentoViewModel);
        void Remove(int id);
    }
}
