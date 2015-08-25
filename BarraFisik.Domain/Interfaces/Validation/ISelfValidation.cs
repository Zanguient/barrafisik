using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Interfaces.Validation
{
    public interface ISelfValidation
    {
        bool IsValid();
        ValidationResult ResultadoValidacao { get; }  
    }
}