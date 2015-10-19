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
    public class DespesasAppService : AppServiceBase<BarraFisikContext>, IDespesasAppService
    {
        private readonly IDespesasService _despesasService;
        private readonly ILogReceitasDespesasService _logReceitasDespesasService;

        public DespesasAppService(IDespesasService despesasService, ILogReceitasDespesasService logReceitasDespesasService)
        {
            _despesasService = despesasService;
            _logReceitasDespesasService = logReceitasDespesasService;
        }

        public void Add(DespesasViewModel despesasViewModel)
        {
            var despesas = Mapper.Map<DespesasViewModel, Despesas>(despesasViewModel);

            despesas.Data = DateTime.Now;

            BeginTransaction();
            _despesasService.Add(despesas);

            //Log
            var logRecDesp = new LogReceitasDespesas
            {
                Data = despesas.Data,
                Valor = despesas.Valor,
                Nome = despesas.Nome,
                Observacao = despesas.Observacao,
                CategoriaFinanceiraId = despesas.CategoriaFinanceiraId.ToString(),
                RegistroId = despesas.DespesasId.ToString()
            };

            _logReceitasDespesasService.AddLog("Cadastro", logRecDesp);
            Commit();
        }

        public DespesasViewModel GetById(Guid id)
        {
            return Mapper.Map<Despesas, DespesasViewModel>(_despesasService.GetById(id));
        }

        public IEnumerable<DespesasViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Despesas>, IEnumerable<DespesasViewModel>>(_despesasService.GetAll());
        }

        public IEnumerable<DespesasViewModel> GetDespesas()
        {
            return Mapper.Map<IEnumerable<Despesas>, IEnumerable<DespesasViewModel>>(_despesasService.GetDespesas());
        }

        public void Update(DespesasViewModel despesasViewModel)
        {
            var despesas = Mapper.Map<DespesasViewModel, Despesas>(despesasViewModel);

            BeginTransaction();
            _despesasService.Update(despesas);

            //Log
            var logRecDesp = new LogReceitasDespesas
            {
                Data = despesas.Data,
                Valor = despesas.Valor,
                Nome = despesas.Nome,
                Observacao = despesas.Observacao,
                CategoriaFinanceiraId = despesas.CategoriaFinanceiraId.ToString(),
                RegistroId = despesas.DespesasId.ToString()
            };

            _logReceitasDespesasService.AddLog("Update", logRecDesp);
            Commit();
        }

        public void Remove(Guid id)
        {
            var despesas = Mapper.Map<DespesasViewModel, Despesas>(GetById(id));

            BeginTransaction();
            _despesasService.Remove(despesas);

            //Log
            var logRecDesp = new LogReceitasDespesas
            {
                Data = despesas.Data,
                Valor = despesas.Valor,
                Nome = despesas.Nome,
                Observacao = despesas.Observacao,
                CategoriaFinanceiraId = despesas.CategoriaFinanceiraId.ToString(),
                RegistroId = despesas.DespesasId.ToString()
            };

            _logReceitasDespesasService.AddLog("Remove", logRecDesp);
            Commit();
        }

        public void Dispose()
        {
            _despesasService.Dispose();
        }
    }
}