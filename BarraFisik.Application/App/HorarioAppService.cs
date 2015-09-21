using System;
using System.Collections.Generic;
using AutoMapper;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Domain.ValueObjects;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Application.App
{
    public class HorarioAppService : AppServiceBase<BarraFisikContext>, IHorarioAppService
    {
        private readonly IHorarioService _horarioService;
        private readonly IClienteService _clienteService;

        public HorarioAppService(IHorarioService horarioService, IClienteService clienteService)
        {
            _horarioService = horarioService;
            _clienteService = clienteService;
        }


        public void Add(HorarioViewModel horarioViewModel)
        {
            var horario = Mapper.Map<HorarioViewModel, Horario>(horarioViewModel);

            BeginTransaction();

            //Verifica se está Desativando o cliente - UPDATE

            //Verifica se cliente não está ativo, e existe horario cadastrado para o mesmo, se existir remove o horario.
            if (_clienteService.GetById(horario.ClienteId).IsAtivo == false && _horarioService.GetById(horario.HorarioId) != null)
            {
                _horarioService.Remove(horario);
            }
            else
            {
                _horarioService.Add(horario);
            }
            
            Commit();
        }

        public HorarioViewModel GetById(Guid id)
        {
            return Mapper.Map<Horario, HorarioViewModel>(_horarioService.GetById(id));
        }

        public IEnumerable<HorarioViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Horario>, IEnumerable<HorarioViewModel>>(_horarioService.GetAll());
        }

        public void Update(ClienteHorarioViewModel clienteHorarioViewModel)
        {
            var horario = Mapper.Map<ClienteHorarioViewModel, Horario>(clienteHorarioViewModel);

            BeginTransaction();

            //Verifica se está Desativando o cliente

            //Verifica se cliente não está ativo, e existe horario cadastrado para o mesmo, se existir remove o horario.
            if (_clienteService.GetById(horario.ClienteId).IsAtivo == false && _horarioService.GetById(horario.HorarioId) != null)
            {
               _horarioService.Remove(horario);
            } else {
                _horarioService.Update(horario);
            }

            Commit();
        }

        public void Remove(HorarioViewModel horarioViewModel)
        {
            var horario = Mapper.Map<HorarioViewModel, Horario>(horarioViewModel);

            BeginTransaction();
            _horarioService.Remove(horario);
            Commit();
        }

        public HorarioViewModel GetHorarioCliente(Guid id)
        {
            return Mapper.Map<Horario, HorarioViewModel>(_horarioService.GetHorarioCliente(id));
        }

        public TotalHorarioViewModel GetHorarios()
        {
            return Mapper.Map<TotalHorario, TotalHorarioViewModel>(_horarioService.GetHorarios());
        }

        public void Dispose()
        {
            _horarioService.Dispose();
        }
    }
}