using BarraFisik.Domain.Entities;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface IClienteService : IServiceBase<Cliente>
    {
        ValidationResult AdicionarCliente(Cliente cliente);
    }
}