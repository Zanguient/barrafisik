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
        private readonly IHorarioRepository _horarioRepository;

        public ClienteService(IClienteRepository clienteRepository, IClienteRepositoryReadOnly clienteRepositoryReadOnly, IHorarioRepository horarioRepository) : base(clienteRepository)
        {
            _clienteRepository = clienteRepository;
            _clienteRepositoryReadOnly = clienteRepositoryReadOnly;
            _horarioRepository = horarioRepository;
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

        public IEnumerable<ClienteHorario> GetClientes()
        {
            return _clienteRepositoryReadOnly.GetAll();
        }

        public IEnumerable<ClienteHorario> GetClientesAll()
        {
            return _clienteRepositoryReadOnly.GetClientesAll();
        }

        public TotalInscritos GetTotalInscritos(int ano)
        {
            return _clienteRepositoryReadOnly.GetTotalInscritos(ano);
        }

        public IEnumerable<Cliente> GetClientesSituacao(string situacao)
        {
            return _clienteRepositoryReadOnly.GetClientesSituacao(situacao);
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

        public void UpdateClientesPendentes(int mes, int ano)
        {
            _clienteRepositoryReadOnly.UpdateClientesPendentes(mes, ano);
        }

        public void InativarClientes(IEnumerable<Cliente> listClientes)
        {            
            foreach (var cliente in listClientes)
            {
                //Delete horario do cliente
                var horario = _horarioRepository.GetHorarioCliente(cliente.ClienteId);
                if(horario != null)
                    _horarioRepository.Remove(horario);

                cliente.Situacao = "Inativo";
                cliente.IsAtivo = false;
                base.Update(cliente);
            }
        }

        public ClienteHorario GetClientePerfil(Guid id)
        {
            return _clienteRepositoryReadOnly.GetClientePerfil(id);
        }

        public ValidationResult VerificaCpfUnico(Cliente cliente)
        {
            var fiscal = new ClienteCpfUnico(_clienteRepository);

            var result = fiscal.Validar(cliente);

            return result;
        }

    }
}