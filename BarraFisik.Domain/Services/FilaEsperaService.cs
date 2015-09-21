using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Services;

namespace BarraFisik.Domain.Services
{
    public class FilaEsperaService : ServiceBase<FilaEspera>, IFilaEsperaService
    {
        private readonly IFilaEsperaRepository _fialEsperaRepository;

        public FilaEsperaService(IFilaEsperaRepository fialEsperaRepository):base(fialEsperaRepository)
        {
            _fialEsperaRepository = fialEsperaRepository;
        }
    }
}