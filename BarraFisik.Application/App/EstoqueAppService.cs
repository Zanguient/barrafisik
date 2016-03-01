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
    public class EstoqueAppService : AppServiceBase<BarraFisikContext>, IEstoqueAppService
    {
        private readonly IEstoqueService _estoqueService;
        private readonly IMovimentacaoEstoqueService _movimentacaoEstoqueService;
        private readonly ILogSistemaService _logSistemaService;

        public EstoqueAppService(IEstoqueService estoqueService, ILogSistemaService logSistemaService, IMovimentacaoEstoqueService movimentacaoEstoqueService)
        {
            _estoqueService = estoqueService;
            _logSistemaService = logSistemaService;
            _movimentacaoEstoqueService = movimentacaoEstoqueService;
        }


        public void Add(EstoqueViewModel estoqueViewModel)
        {
            var estoque = Mapper.Map<EstoqueViewModel, Estoque>(estoqueViewModel);

            BeginTransaction();
            _estoqueService.Add(estoque);

            _logSistemaService.AddLog("Estoque", estoqueViewModel.EstoqueId, "Cadastro", "");
            Commit();
        }

        public EstoqueViewModel GetById(Guid id)
        {
            return Mapper.Map<Estoque, EstoqueViewModel>(_estoqueService.GetById(id));
        }

        public IEnumerable<EstoqueViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Estoque>, IEnumerable<EstoqueViewModel>>(_estoqueService.GetAll());
        }

        public IEnumerable<EstoqueViewModel> GetEstoques()
        {
            return Mapper.Map<IEnumerable<Estoque>, IEnumerable<EstoqueViewModel>>(_estoqueService.GetEstoques());
        }

        public IEnumerable<EstoqueViewModel> GetByArmazem(Guid id)
        {
            return Mapper.Map<IEnumerable<Estoque>, IEnumerable<EstoqueViewModel>>(_estoqueService.GetByArmazem(id));
        }

        public bool ExisteEstoque(Guid armazemId, Guid produtoId)
        {
            return _estoqueService.ExisteEstoque(armazemId, produtoId);
        }

        public void Update(EstoqueViewModel estoqueViewModel)
        {
            var estoque = Mapper.Map<EstoqueViewModel, Estoque>(estoqueViewModel);

            BeginTransaction();
            _estoqueService.Update(estoque);

            _logSistemaService.AddLog("Estoque", estoqueViewModel.EstoqueId, "Update", "");
            Commit();
        }

        public void Remove(Guid id)
        {
            var estoque = Mapper.Map<EstoqueViewModel, Estoque>(GetById(id));

            BeginTransaction();
            
            //Remove todas suas movimentações
            foreach (var m in _movimentacaoEstoqueService.GetByEstoque(estoque.EstoqueId))
            {               
                _movimentacaoEstoqueService.Remove(m);
            }

            _estoqueService.Remove(estoque);

            _logSistemaService.AddLog("Estoque", estoque.EstoqueId, "Remove", "");
            Commit();
        }        

        public void Dispose()
        {
            _estoqueService.Dispose();
        }
    }
}
