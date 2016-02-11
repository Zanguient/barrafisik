using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.Specification.Mensalidades;
using BarraFisik.Domain.Validation.Base;

namespace BarraFisik.Domain.Validation.Mensalidades
{
    public class MensalidadeUnica : FiscalBase<Entities.Receitas>
    {
        public MensalidadeUnica(IReceitasRepositoryReadOnly receitasRepositoryReadOnly, IReceitasRepository receitasRepository)
        {
            var mensalidadeUnica = new MensalidadeUnicaMensal(receitasRepositoryReadOnly, receitasRepository);

            base.AdicionarRegra("MensalidadeUnico", new Regra<Entities.Receitas>(mensalidadeUnica, "Já existe mensalidade vinculada a esta data para este cliente"));
        }
    }
}