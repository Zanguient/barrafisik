using System.Linq;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.Interfaces.Specification;

namespace BarraFisik.Domain.Specification.Mensalidades
{
    public class MensalidadeUnicaMensal : ISpecification<Entities.Mensalidades>
    {
        private readonly IMensalidadesRepository _mensalidadesRepository;
        private readonly IMensalidadesRepositoryReadOnly _mensalidadesRepositoryReadOnly;

        public MensalidadeUnicaMensal(IMensalidadesRepositoryReadOnly mensalidadesRepositoryReadOnly, IMensalidadesRepository mensalidadesRepository)
        {
            _mensalidadesRepositoryReadOnly = mensalidadesRepositoryReadOnly;
            _mensalidadesRepository = mensalidadesRepository;
        }


        public bool IsSatisfiedBy(Entities.Mensalidades mensalidade)
        {
            //Caso esteja editando
            var mensalidadeBase = _mensalidadesRepository.GetById(mensalidade.MensalidadesId);

            if (mensalidadeBase != null && mensalidadeBase.MesReferencia == mensalidade.MesReferencia &&
                mensalidadeBase.AnoReferencia == mensalidade.AnoReferencia)
                return true;
            return !_mensalidadesRepositoryReadOnly.ExisteMensalidade(mensalidade);
        }
    }
}