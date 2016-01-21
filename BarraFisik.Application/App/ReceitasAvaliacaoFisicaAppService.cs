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
    public class ReceitasAvaliacaoFisicaAppService : AppServiceBase<BarraFisikContext>, IReceitasAvaliacaoFisicaAppService
    {

        private readonly IReceitasAvaliacaoFisicaService _receitasAvaliacaoFisicaService;
        private readonly ILogSistemaService _logSistemaService;

        public ReceitasAvaliacaoFisicaAppService(IReceitasAvaliacaoFisicaService receitasAvaliacaoFisicaService, ILogSistemaService logSistemaService)
        {
            _receitasAvaliacaoFisicaService = receitasAvaliacaoFisicaService;
            _logSistemaService = logSistemaService;
        }


        public void Add(ReceitasAvaliacaoFisicaViewModel receitasAvaliacaoFisicaViewModel)
        {
            var receitaAvaliacaoFisica = Mapper.Map<ReceitasAvaliacaoFisicaViewModel, ReceitasAvaliacaoFisica>(receitasAvaliacaoFisicaViewModel);

            BeginTransaction();
            _receitasAvaliacaoFisicaService.Add(receitaAvaliacaoFisica);

            _logSistemaService.AddLog("ReceitasAvaliacaoFisica", receitasAvaliacaoFisicaViewModel.ReceitasAvaliacaoFisicaId, "Cadastro", "Valor:" + receitaAvaliacaoFisica.Valor + " / Data: " + receitaAvaliacaoFisica.DataPagamento + " / Tipo pagamento: " + receitaAvaliacaoFisica.TipoPagamentoId);
            Commit();
        }

        public ReceitasAvaliacaoFisicaViewModel GetById(Guid id)
        {
            return Mapper.Map<ReceitasAvaliacaoFisica, ReceitasAvaliacaoFisicaViewModel>(_receitasAvaliacaoFisicaService.GetById(id));
        }

        public IEnumerable<ReceitasAvaliacaoFisicaViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<ReceitasAvaliacaoFisica>, IEnumerable<ReceitasAvaliacaoFisicaViewModel>>(_receitasAvaliacaoFisicaService.GetAll());
        }

        public IEnumerable<ReceitasAvaliacaoFisicaViewModel> GetByCliente(Guid id)
        {
            return Mapper.Map<IEnumerable<ReceitasAvaliacaoFisica>, IEnumerable<ReceitasAvaliacaoFisicaViewModel>>(_receitasAvaliacaoFisicaService.GetByCliente(id));
        }

        public void Update(ReceitasAvaliacaoFisicaViewModel receitasAvaliacaoFisicaViewModel)
        {
            var receitaAvaliacaoFisica = Mapper.Map<ReceitasAvaliacaoFisicaViewModel, ReceitasAvaliacaoFisica>(receitasAvaliacaoFisicaViewModel);

            BeginTransaction();
            _receitasAvaliacaoFisicaService.Add(receitaAvaliacaoFisica);

            _logSistemaService.AddLog("ReceitasAvaliacaoFisica", receitasAvaliacaoFisicaViewModel.ReceitasAvaliacaoFisicaId, "Update", "Valor:" + receitaAvaliacaoFisica.Valor + " / Data: " + receitaAvaliacaoFisica.DataPagamento + " / Tipo pagamento:" + receitaAvaliacaoFisica.TipoPagamentoId);
            Commit();
        }

        public void Remove(Guid id)
        {
            var receitaAvaliacaoFisica = Mapper.Map<ReceitasAvaliacaoFisicaViewModel, ReceitasAvaliacaoFisica>(GetById(id));

            BeginTransaction();
            _receitasAvaliacaoFisicaService.Remove(receitaAvaliacaoFisica);

            _logSistemaService.AddLog("ReceitasAvaliacaoFisica", id, "Remove", "Valor:" + receitaAvaliacaoFisica.Valor + " / Data: " + receitaAvaliacaoFisica.DataPagamento + " / Tipo pagamento:" + receitaAvaliacaoFisica.TipoPagamentoId);
            Commit();
        }

        public void Dispose()
        {
            _receitasAvaliacaoFisicaService.Dispose();
        }
    }
}