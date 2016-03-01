using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Services
{
    public class VendasService : ServiceBase<Vendas>, IVendasService
    {
        private readonly IVendasRepository _vendasRepository;
        private readonly IVendasRepositoryReadOnly _vendasRepositoryReadOnly;

        public VendasService(IVendasRepository vendasRepository, IVendasRepositoryReadOnly vendasRepositoryReadOnly):base(vendasRepository)
        {
            _vendasRepository = vendasRepository;
            _vendasRepositoryReadOnly = vendasRepositoryReadOnly;
        }

        public IEnumerable<Vendas> GetVendas()
        {
            return _vendasRepository.GetVendas();            
        }

        public IEnumerable<Vendas> GetPendentes(int mes, int ano)
        {
            return _vendasRepository.GetPendentes(mes, ano);
        }

        public IEnumerable<Vendas> SearchVendas(SearchVendas sv)
        {
            return _vendasRepositoryReadOnly.SearchVendas(sv);
        }

        public List<int> GetVendasAnual(int ano)
        {
            return _vendasRepositoryReadOnly.GetVendasAnual(ano);
        }

        public IEnumerable<Vendas> GetVendasByCliente(Guid idCliente)
        {
            return _vendasRepository.GetVendasByCliente(idCliente);
        }
    }
}
