using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.Interfaces.Services;

namespace BarraFisik.Domain.Services
{
    public class ValoresService : ServiceBase<Valores>, IValoresService
    {
        private readonly IValoresRepository _valoresRepository;
        private readonly IValoresRepositoryReadOnly _valoresRepositoryReadOnly;

        public ValoresService(IValoresRepository valoresRepository, IValoresRepositoryReadOnly valoresRepositoryOnly) :base(valoresRepository)
        {
            _valoresRepository = valoresRepository;
            _valoresRepositoryReadOnly = valoresRepositoryOnly;
        }

        public Valores GetValorCliente(int qtdDias, int horario)
        {
            return _valoresRepositoryReadOnly.GetValorCliente(qtdDias, horario);
        }
    }
}