using System.Linq;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Specification;

namespace BarraFisik.Domain.Specification.Clientes
{
    public class ClientePossuiCPFUnico : ISpecification<Cliente>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientePossuiCPFUnico(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public bool IsSatisfiedBy(Cliente cliente)
        {
            //Cadastro            
            var clienteBase = _clienteRepository.GetById(cliente.ClienteId);

            //CPF Vazio
            if(string.IsNullOrEmpty(cliente.Cpf))
                return true;

            // Se forem iguais estou editando sem alterar o cpf do mesmo
            if (clienteBase != null && clienteBase.Cpf == cliente.Cpf)
                return true;
            return !_clienteRepository.Find(c => c.Cpf == cliente.Cpf).Any();
        }
    }
}