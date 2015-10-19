using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Services;

namespace BarraFisik.Domain.Services
{
    public class CategoriaFinanceiraService : ServiceBase<CategoriaFinanceira>, ICategoriaFinanceiraService
    {
        private readonly ICategoriaFinanceiraRepository _categoriaFinanceiraRepository;

        public CategoriaFinanceiraService(ICategoriaFinanceiraRepository categoriaFinanceiraRepository):base(categoriaFinanceiraRepository)
        {
            _categoriaFinanceiraRepository = categoriaFinanceiraRepository;
        }

        public IEnumerable<CategoriaFinanceira> GetByTipo(string tipo)
        {
            return _categoriaFinanceiraRepository.GetByTipo(tipo);
        }
    }
}