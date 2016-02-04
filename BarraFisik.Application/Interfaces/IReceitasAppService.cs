using System;
using System.Collections.Generic;
using BarraFisik.Application.ViewModels;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Application.Interfaces
{
    public interface IReceitasAppService : IAppServiceBase<BarraFisikContext>
    {
        void Add(ReceitasViewModel receitasViewModel);
        ReceitasViewModel GetById(Guid id);
        IEnumerable<ReceitasViewModel> SearchReceitas(SearchReceitasViewModel sr);
        IEnumerable<ReceitasViewModel> GetAll();
        IEnumerable<ReceitasViewModel> GetReceitas();
        void Update(ReceitasViewModel receitasViewModel);
        void Remove(Guid id);
    }
}