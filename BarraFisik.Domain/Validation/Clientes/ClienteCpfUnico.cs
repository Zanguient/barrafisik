using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Specification.Clientes;
using BarraFisik.Domain.Validation.Base;

namespace BarraFisik.Domain.Validation.Clientes
{
    public class ClienteCpfUnico : FiscalBase<Cliente>
    {
        public ClienteCpfUnico(IClienteRepository clienteRepository)
        {
            var cpfUnico = new ClientePossuiCPFUnico(clienteRepository);

            base.AdicionarRegra("CPFUnico", new Regra<Cliente>(cpfUnico, "O CPF informado já está cadastrado no sistema"));
        }
    }
}