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
        private readonly ILogSistemaService _logSistemaService;

        public HorarioAppService(IHorarioService horarioService, IClienteService clienteService, ILogSistemaService logSistemaService)
        {
            _horarioService = horarioService;
            _clienteService = clienteService;
            _logSistemaService = logSistemaService;
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
                _logSistemaService.AddLog("Horario", horarioViewModel.HorarioId, "Remove", "Cadastro de cliente e o mesmo não está ativo e existe horário vinculado ao mesmo. Cliente:" +horario.ClienteId);
            }
            else
            {
                _horarioService.Add(horario);
                _logSistemaService.AddLog("Horario", horarioViewModel.HorarioId, "Cadastro", "Cliente:" + horarioViewModel.ClienteId);
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
               _logSistemaService.AddLog("Horario", clienteHorarioViewModel.HorarioId, "Remove", "Update de cliente e o mesmo não está ativo e existe horário vinculado ao mesmo. Cliente:" + horario.ClienteId);
            } else {
                _horarioService.Update(horario);
                _logSistemaService.AddLog("Horario", clienteHorarioViewModel.HorarioId, "Update", "Cliente:" + clienteHorarioViewModel.ClienteId);
            }

            Commit();
        }

        public void Remove(HorarioViewModel horarioViewModel)
        {
            var horario = Mapper.Map<HorarioViewModel, Horario>(horarioViewModel);

            BeginTransaction();
            _horarioService.Remove(horario);

            _logSistemaService.AddLog("Horario", horarioViewModel.HorarioId, "Remove", "Delete horario:" + horario.ClienteId);
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