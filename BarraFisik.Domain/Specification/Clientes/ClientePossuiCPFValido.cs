using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Specification;
using BarraFisik.Domain.Validation.Documentos;

namespace BarraFisik.Domain.Specification.Clientes
{
    public class ClientePossuiCPFValido : ISpecification<Cliente>
    {
        public bool IsSatisfiedBy(Cliente cliente)
        {
            return CPFValidation.Validar(cliente.Cpf);
        }
    }
}