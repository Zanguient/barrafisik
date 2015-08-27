using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Services
{
    public class ClienteService : ServiceBase<Cliente>, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteRepositoryReadOnly _clienteRepositoryReadOnly;

        public ClienteService(IClienteRepository clienteRepository, IClienteRepositoryReadOnly clienteRepositoryReadOnly) : base(clienteRepository)
        {
            _clienteRepository = clienteRepository;
            _clienteRepositoryReadOnly = clienteRepositoryReadOnly;
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

        public IEnumerable<Cliente> GetClientes()
        {
            return _clienteRepositoryReadOnly.GetAll();
        }

    }
}