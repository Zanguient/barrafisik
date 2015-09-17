using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.Specification.Mensalidades;
using BarraFisik.Domain.Validation.Base;

namespace BarraFisik.Domain.Validation.Mensalidades
{
    public class MensalidadeUnica : FiscalBase<Entities.Mensalidades>
    {
        public MensalidadeUnica(IMensalidadesRepositoryReadOnly mensalidadesRepositoryReadOnly, IMensalidadesRepository mensalidadesRepository)
        {
            var mensalidadeUnica = new MensalidadeUnicaMensal(mensalidadesRepositoryReadOnly, mensalidadesRepository);

            base.AdicionarRegra("MensalidadeUnico", new Regra<Entities.Mensalidades>(mensalidadeUnica, "Já existe mensalidade vinculada a esta data"));
        }
    }
}