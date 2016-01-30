using System;
using System.Collections.Generic;
using BarraFisik.Application.ViewModels;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Application.Interfaces
{
    public interface IDespesasAppService : IAppServiceBase<BarraFisikContext>
    {
        void Add(DespesasViewModel despesasViewModel);
        DespesasViewModel GetById(Guid id);
        IEnumerable<DespesasViewModel> GetAll();
        IEnumerable<DespesasViewModel> GetDespesas();
        IEnumerable<DespesasViewModel> GetDespesasAll();
        IEnumerable<DespesasViewModel> SearchDespesas(SearchDespesasViewModel sd);
        void Update(DespesasViewModel despesasViewModel);
        void Remove(Guid id);
    }
}