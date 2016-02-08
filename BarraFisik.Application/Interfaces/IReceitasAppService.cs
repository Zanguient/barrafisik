using System;
using System.Collections.Generic;
using BarraFisik.Application.ViewModels;
using BarraFisik.Infra.Data.Context;
using BarraFisik.Application.Validation;

namespace BarraFisik.Application.Interfaces
{
    public interface IReceitasAppService : IAppServiceBase<BarraFisikContext>
    {
        void Add(ReceitasViewModel receitasViewModel);
        ValidationAppResult AddMensalidade(ReceitasViewModel receitasViewModel);
        ReceitasViewModel GetById(Guid id);
        IEnumerable<ReceitasViewModel> SearchReceitas(SearchReceitasViewModel sr);
        IEnumerable<ReceitasViewModel> GetAll();
        IEnumerable<ReceitasViewModel> GetMensalidadesCliente(Guid? idCliente);
        IEnumerable<ReceitasViewModel> GetAvaliacaoCliente(Guid? idCliente);
        IEnumerable<ReceitasViewModel> GetReceitas();
        ValidationAppResult UpdateMensalidade(ReceitasViewModel receitasViewModel);
        void Update(ReceitasViewModel receitasViewModel);
        void Remove(Guid id);
        void RemoveMensalidade(Guid id);
    }
}