using System;
using System.Collections.Generic;
using BarraFisik.Application.Validation;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.Application.Interfaces
{
    public interface IClienteAppService : IDisposable
    {
        ValidationAppResult Add(ClienteHorarioViewModel clienteHorarioViewModel);
        ClienteViewModel GetById(Guid id);
        IEnumerable<ClienteHorarioViewModel> GetAll();
        IEnumerable<ClienteHorarioViewModel> GetClientesAll();
        ValidationAppResult Update(ClienteHorarioViewModel clienteHorarioViewModel);
        void Remove(ClienteViewModel clienteViewModel);
        ClienteHorarioViewModel GetByClienteId(Guid id);

        ClienteViewModel GetClienteHorario(Guid id);
        IEnumerable<ClienteViewModel> GetAniversariantes(int mes);

        void UpdateClientesPendentes(int mes, int ano);
        IEnumerable<ClienteViewModel> GetClientesSituacao(string situacao);
        void InativarClientes(IEnumerable<ClienteViewModel> listClientes);
        TotalInscritosViewModel GetTotalInscritos(int ano);

       ClienteHorarioViewModel GetClientePerfil(Guid id);
    }
}