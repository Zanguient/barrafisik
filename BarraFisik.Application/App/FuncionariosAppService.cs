using System.Collections.Generic;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Infra.Data.Context;
using AutoMapper;
using BarraFisik.Domain.Entities;
using System;

namespace BarraFisik.Application.App
{
    public class FuncionariosAppService : AppServiceBase<BarraFisikContext>, IFuncionariosAppService
    {
        private readonly IFuncionariosService _funcionariosService;
        private readonly ILogSistemaService _logSistemaService;

        public FuncionariosAppService(IFuncionariosService funcionariosService, ILogSistemaService logSistemaService)
        {
            _funcionariosService = funcionariosService;
            _logSistemaService = logSistemaService;
        }

        public void Add(FuncionariosViewModel funcionariosViewModel)
        {
            var funcionario = Mapper.Map<FuncionariosViewModel, Funcionarios>(funcionariosViewModel);

            BeginTransaction();
            _funcionariosService.Add(funcionario);

            _logSistemaService.AddLog("Funcionarios", funcionariosViewModel.FuncionarioId, "Cadastro", "Nome: " + funcionariosViewModel.Nome + " Status: "+ funcionariosViewModel.isAtivo);
            Commit();
        }

        public FuncionariosViewModel GetById(Guid id)
        {
            return Mapper.Map<Funcionarios, FuncionariosViewModel>(_funcionariosService.GetById(id));
        }

        public IEnumerable<FuncionariosViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Funcionarios>, IEnumerable<FuncionariosViewModel>>(_funcionariosService.GetAll());
        }

        public IEnumerable<FuncionariosViewModel> GetAllAtivos()
        {
            return Mapper.Map<IEnumerable<Funcionarios>, IEnumerable<FuncionariosViewModel>>(_funcionariosService.GetAllAtivos());
        } 

        public void Update(FuncionariosViewModel funcionariosViewModel)
        {
            var funcionario = Mapper.Map<FuncionariosViewModel, Funcionarios>(funcionariosViewModel);

            BeginTransaction();
            _funcionariosService.Update(funcionario);

            _logSistemaService.AddLog("Funcionarios", funcionariosViewModel.FuncionarioId, "Update", "Nome: " + funcionariosViewModel.Nome + " Status: " + funcionariosViewModel.isAtivo);
            Commit();
        }

        public void Remove(Guid id)
        {
            var funcionario = Mapper.Map<FuncionariosViewModel, Funcionarios>(GetById(id));

            BeginTransaction();
            _funcionariosService.Remove(funcionario);

            _logSistemaService.AddLog("Funcionarios", funcionario.FuncionarioId, "Remove", "Nome: " + funcionario.Nome + " Status: " + funcionario.isAtivo);
            Commit();
        }

        public void Dispose()
        {
            _funcionariosService.Dispose();
        }
    }
}
