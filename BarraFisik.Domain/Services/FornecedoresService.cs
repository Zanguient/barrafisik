using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Services;

namespace BarraFisik.Domain.Services
{
    public class FornecedoresService : ServiceBase<Fornecedores>, IFornecedoresService
    {
        private readonly IFornecedoresRepository _fornecedoresRepository;

        public FornecedoresService(IFornecedoresRepository fornecedoresRepository) :base(fornecedoresRepository)
        {
            _fornecedoresRepository = fornecedoresRepository;
        }

        public IEnumerable<Fornecedores> GetAllAtivos()
        {
            return _fornecedoresRepository.GetAllAtivos();
        } 
    }
}
