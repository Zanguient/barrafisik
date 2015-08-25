using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Services
{
    public class ClienteService : ServiceBase<Cliente>, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository) : base(clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public ValidationResult AdicionarCliente(Cliente cliente)
        {
            var resultado = new ValidationResult();

            if (!cliente.IsValid())
            {
                resultado.AdicionarErro(cliente.ResultadoValidacao);
                return resultado;
            }

            //resultado = VerificaCpfJaCadastrado(aluno);
            //if (!resultado.IsValid)
            //{
            //    resultado.AdicionarErro(aluno.ResultadoValidacao);
            //    return resultado;
            //}

            base.Add(cliente);
            return resultado;
        }
    }
}