using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Services;

namespace BarraFisik.Domain.Services
{
    public class FuncionariosService : ServiceBase<Funcionarios>, IFuncionariosService
    {
        private readonly IFuncionariosRepository _funcionariosRepository;

        public FuncionariosService(IFuncionariosRepository funcionariosRepository) :base(funcionariosRepository)
        {
            _funcionariosRepository = funcionariosRepository;
        }

        public IEnumerable<Funcionarios> GetAllAtivos()
        {
            return _funcionariosRepository.GetAllAtivos();
        } 
    }
}
