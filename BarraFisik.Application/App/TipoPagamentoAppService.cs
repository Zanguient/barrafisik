using System.Collections.Generic;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Infra.Data.Context;
using AutoMapper;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Application.App
{
    public class TipoPagamentoAppService : AppServiceBase<BarraFisikContext>, ITipoPagamentoAppService
    {
        private readonly ITipoPagamentoService _tipoPagamentoService;

        public TipoPagamentoAppService(ITipoPagamentoService tipoPagamentoService)
        {
            _tipoPagamentoService = tipoPagamentoService;
        }

        public void Add(TipoPagamentoViewModel tipoPagamentoViewModel)
        {
            var tipoPagamento = Mapper.Map<TipoPagamentoViewModel, TipoPagamento>(tipoPagamentoViewModel);

            BeginTransaction();
            _tipoPagamentoService.Add(tipoPagamento);
            Commit();
        }

        public TipoPagamentoViewModel GetById(int id)
        {
            return Mapper.Map<TipoPagamento, TipoPagamentoViewModel>(_tipoPagamentoService.GetByIdInt(id));
        }

        public IEnumerable<TipoPagamentoViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<TipoPagamento>, IEnumerable<TipoPagamentoViewModel>>(_tipoPagamentoService.GetAll());
        }

        public void Update(TipoPagamentoViewModel tipoPagamentoViewModel)
        {
            var tipoPagamento = Mapper.Map<TipoPagamentoViewModel, TipoPagamento>(tipoPagamentoViewModel);

            BeginTransaction();
            _tipoPagamentoService.Update(tipoPagamento);
            Commit();
        }

        public void Remove(int id)
        {
            BeginTransaction();
            _tipoPagamentoService.Delete(id);
            Commit();
        }

       
    }
}
