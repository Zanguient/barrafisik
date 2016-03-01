using System;
using System.Collections.Generic;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.Application.Interfaces
{
    public interface IVendasAppService : IDisposable
    {
        void Add(VendasViewModel vendasViewModel);
        VendasViewModel GetById(Guid id);
        IEnumerable<VendasViewModel> GetAll();
        IEnumerable<VendasViewModel> GetVendas();
        IEnumerable<VendasViewModel> GetPendentes(int mes, int ano);
        IEnumerable<VendasViewModel> SearchVendas(SearchVendasViewModel sr);
        void Update(VendasViewModel vendasViewModel);
        void Remove(Guid id);
        IList<int> GetVendasAnual(int ano);
        IEnumerable<VendasViewModel> GetVendasByCliente(Guid idCliente);
    }
}