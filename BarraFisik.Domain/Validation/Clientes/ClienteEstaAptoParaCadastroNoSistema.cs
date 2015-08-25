using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Specification.Clientes;
using BarraFisik.Domain.Validation.Base;

namespace BarraFisik.Domain.Validation.Clientes
{
    public class ClienteEstaAptoParaCadastroNoSistema : FiscalBase<Cliente>
    {
        public ClienteEstaAptoParaCadastroNoSistema()
        {
            var clienteCPF = new ClientePossuiCPFValido();

            base.AdicionarRegra("CPFValido", new Regra<Cliente>(clienteCPF, "CPF informado é inválido"));
        }
    }
}