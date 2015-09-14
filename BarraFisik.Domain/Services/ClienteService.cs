using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Domain.Specification.Clientes;
using BarraFisik.Domain.Validation.Clientes;
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

            resultado = VerificaCpfUnico(cliente);
            if (!resultado.IsValid)
            {
                resultado.AdicionarErro(cliente.ResultadoValidacao);
                return resultado;
            }

            base.Add(cliente);
            return resultado;
        }

        public ValidationResult AtualizarCliente(Cliente cliente)
        {
            var resultado = new ValidationResult();

            if (!cliente.IsValid())
            {
                resultado.AdicionarErro(cliente.ResultadoValidacao);
                return resultado;
            }

            resultado = VerificaCpfUnico(cliente);
            if (!resultado.IsValid)
            {
                resultado.AdicionarErro(cliente.ResultadoValidacao);
                return resultado;
            }

            base.Update(cliente);
            return resultado;
        }

        public IEnumerable<Cliente> GetClientes()
        {
            return _clienteRepositoryReadOnly.GetAll();
        }

        public Cliente GetClienteHorario(Guid id)
        {
            return _clienteRepository.GetClienteHorario(id);
        }

        public ClienteHorario GetByClienteId(Guid id)
        {
            return _clienteRepositoryReadOnly.GetByClienteId(id);
        }

        public IEnumerable<Cliente> GetAniversariantes(int mes)
        {
            return _clienteRepository.GetAniversariantes(mes);
        }

        public ValidationResult VerificaCpfUnico(Cliente cliente)
        {
            var fiscal = new ClienteCpfUnico(_clienteRepository);

            var result = fiscal.Validar(cliente);

            return result;
        }

    }
}