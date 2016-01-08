using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Specification;
using BarraFisik.Domain.Validation.Documentos;

namespace BarraFisik.Domain.Specification.Clientes
{
    public class ClientePossuiCPFValido : ISpecification<Cliente>
    {
        public bool IsSatisfiedBy(Cliente cliente)
        {
            //CPF vazio
            if (string.IsNullOrEmpty(cliente.Cpf))
                return true;

            return CPFValidation.Validar(cliente.Cpf);
        }
    }
}