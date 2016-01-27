using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Services;

namespace BarraFisik.Domain.Services
{
    public class ArmazemService : ServiceBase<Armazem>, IArmazemService
    {
        private readonly IArmazemRepository _armazemRepository;

        public ArmazemService(IArmazemRepository armazemRepository):base(armazemRepository)
        {
            _armazemRepository = armazemRepository;
        }
    }
}
