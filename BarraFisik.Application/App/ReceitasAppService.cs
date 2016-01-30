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
    public class ReceitasAppService : AppServiceBase<BarraFisikContext>, IReceitasAppService
    {
        private readonly IReceitasService _receitasService;
        private readonly ILogReceitasDespesasService _logReceitasDespesasService;

        public ReceitasAppService(IReceitasService receitasService, ILogReceitasDespesasService logReceitasDespesasService)
        {
            _receitasService = receitasService;
            _logReceitasDespesasService = logReceitasDespesasService;
        }

        public void Add(ReceitasViewModel receitasViewModel)
        {
            var receitas = Mapper.Map<ReceitasViewModel, Receitas>(receitasViewModel);            

            //BeginTransaction();

            //_receitasService.Add(receitas);

            ////Log
            //var logRecDesp = new LogReceitasDespesas
            //{
            //    Data = receitas.Data,
            //    Valor = receitas.Valor,
            //    Nome = receitas.Nome,
            //    Observacao = receitas.Observacao,
            //    CategoriaFinanceiraId = receitas.CategoriaFinanceiraId.ToString(),
            //    RegistroId = receitas.ReceitasId.ToString()
            //};

            //_logReceitasDespesasService.AddLog("Cadastro", logRecDesp);
            //Commit();
        }

        public ReceitasViewModel GetById(Guid id)
        {
            return Mapper.Map<Receitas, ReceitasViewModel>(_receitasService.GetById(id));
        }

        public IEnumerable<ReceitasViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Receitas>, IEnumerable<ReceitasViewModel>>(_receitasService.GetAll());
        }

        public IEnumerable<ReceitasViewModel> GetReceitas()
        {
            return Mapper.Map<IEnumerable<Receitas>, IEnumerable<ReceitasViewModel>>(_receitasService.GetReceitas());
        }

        public void Update(ReceitasViewModel receitasViewModel)
        {
            var receitas = Mapper.Map<ReceitasViewModel, Receitas>(receitasViewModel);

            //BeginTransaction();
            //_receitasService.Update(receitas);

            ////Log
            //var logRecDesp = new LogReceitasDespesas
            //{
            //    Data = receitas.Data,
            //    Valor = receitas.Valor,
            //    Nome = receitas.Nome,
            //    Observacao = receitas.Observacao,
            //    CategoriaFinanceiraId = receitas.CategoriaFinanceiraId.ToString(),
            //    RegistroId = receitas.ReceitasId.ToString()
            //};

            //_logReceitasDespesasService.AddLog("Cadastro", logRecDesp);
            //Commit();
        }

        public void Remove(Guid id)
        {
            var receitas = Mapper.Map<ReceitasViewModel, Receitas>(GetById(id));

            //BeginTransaction();
            //_receitasService.Remove(receitas);

            ////Log
            //var logRecDesp = new LogReceitasDespesas
            //{
            //    Data = receitas.Data,
            //    Valor = receitas.Valor,
            //    Nome = receitas.Nome,
            //    Observacao = receitas.Observacao,
            //    CategoriaFinanceiraId = receitas.CategoriaFinanceiraId.ToString(),
            //    RegistroId = receitas.ReceitasId.ToString()
            //};

            //_logReceitasDespesasService.AddLog("Cadastro", logRecDesp);
            //Commit();
        }

        public void Dispose()
        {
            _receitasService.Dispose();
        }
    }
}