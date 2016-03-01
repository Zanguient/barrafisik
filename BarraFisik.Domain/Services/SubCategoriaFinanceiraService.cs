using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Services;

namespace BarraFisik.Domain.Services
{
    public class SubCategoriaFinanceiraService : ServiceBase<SubCategoriaFinanceira>, ISubCategoriaFinanceiraService
    {
        private readonly ISubCategoriaFinanceiraRepository _subCategoriaFinanceiraRepository;

        public SubCategoriaFinanceiraService(ISubCategoriaFinanceiraRepository subCategoriaFinanceiraRepository) :base(subCategoriaFinanceiraRepository)
        {
            _subCategoriaFinanceiraRepository = subCategoriaFinanceiraRepository;
        }

        public IEnumerable<SubCategoriaFinanceira> GetByCategoria(Guid idCategoria)
        {
            return _subCategoriaFinanceiraRepository.GetByCategoria(idCategoria);
        }
    }
}