using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Interfaces.Validation
{
    public interface IFiscal<in TEntity>
    {
        ValidationResult Validar(TEntity entity);
    }
}