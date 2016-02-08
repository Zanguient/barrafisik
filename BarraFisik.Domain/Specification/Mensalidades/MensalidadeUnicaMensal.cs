using System.Linq;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.Interfaces.Specification;

namespace BarraFisik.Domain.Specification.Mensalidades
{
    public class MensalidadeUnicaMensal : ISpecification<Entities.Receitas>
    {
        private readonly IReceitasRepository _receitasRepository;
        private readonly IReceitasRepositoryReadOnly _receitasRepositoryReadOnly;

        public MensalidadeUnicaMensal(IReceitasRepositoryReadOnly receitasRepositoryReadOnly, IReceitasRepository receitasRepository)
        {
            _receitasRepositoryReadOnly = receitasRepositoryReadOnly;
            _receitasRepository = receitasRepository;
        }


        public bool IsSatisfiedBy(Entities.Receitas mensalidade)
        {
            //Caso esteja editando
            var mensalidadeBase = _receitasRepository.GetById(mensalidade.ReceitasId);

            if (mensalidadeBase != null && mensalidadeBase.MesReferencia == mensalidade.MesReferencia &&
                mensalidadeBase.AnoReferencia == mensalidade.AnoReferencia)
                return true;
            return !_receitasRepositoryReadOnly.ExisteMensalidade(mensalidade);
        }
    }
}