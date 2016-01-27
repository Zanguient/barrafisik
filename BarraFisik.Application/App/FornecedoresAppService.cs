using System;
using System.Collections.Generic;
using AutoMapper;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Application.App
{
    public class FornecedoresAppService : AppServiceBase<BarraFisikContext>, IFornecedoresAppService
    {
        private readonly IFornecedoresService _fornecedoresService;
        private readonly ILogSistemaService _logSistemaService;

        public FornecedoresAppService(IFornecedoresService fornecedoresService, ILogSistemaService logSistemaService)
        {
            _fornecedoresService = fornecedoresService;
            _logSistemaService = logSistemaService;
        }

        public void Add(FornecedoresViewModel fornecedoresViewModel)
        {
            var fornecedor = Mapper.Map<FornecedoresViewModel, Fornecedores>(fornecedoresViewModel);

            BeginTransaction();
            _fornecedoresService.Add(fornecedor);

            _logSistemaService.AddLog("Fornecedores", fornecedoresViewModel.FornecedorId, "Cadastro", "Nome: " + fornecedoresViewModel.Nome + " Status: " + fornecedoresViewModel.isAtivo);
            Commit();
        }

        public FornecedoresViewModel GetById(Guid id)
        {
            return Mapper.Map<Fornecedores, FornecedoresViewModel>(_fornecedoresService.GetById(id));
        }

        public IEnumerable<FornecedoresViewModel> GetAll()
        {
            return
                Mapper.Map<IEnumerable<Fornecedores>, IEnumerable<FornecedoresViewModel>>(_fornecedoresService.GetAll());
        }

        public IEnumerable<FornecedoresViewModel> GetAllAtivos()
        {
            return
                Mapper.Map<IEnumerable<Fornecedores>, IEnumerable<FornecedoresViewModel>>(_fornecedoresService.GetAllAtivos());
        }

        public void Update(FornecedoresViewModel fornecedoresViewModel)
        {
            var fornecedor = Mapper.Map<FornecedoresViewModel, Fornecedores>(fornecedoresViewModel);

            BeginTransaction();
            _fornecedoresService.Update(fornecedor);

            _logSistemaService.AddLog("Fornecedores", fornecedoresViewModel.FornecedorId, "Update", "Nome: " + fornecedoresViewModel.Nome + " Status: " + fornecedoresViewModel.isAtivo);
            Commit();
        }

        public void Remove(Guid id)
        {
            var fornecedor = Mapper.Map<FornecedoresViewModel, Fornecedores>(GetById(id));

            BeginTransaction();
            _fornecedoresService.Remove(fornecedor);

            _logSistemaService.AddLog("Fornecedores", fornecedor.FornecedorId, "Remove", "Nome: " + fornecedor.Nome + " Status: " + fornecedor.isAtivo);
            Commit();
        }

        public void Dispose()
        {
            _fornecedoresService.Dispose();
        }
    }
}