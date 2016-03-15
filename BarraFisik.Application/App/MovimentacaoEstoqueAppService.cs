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
    public class MovimentacaoEstoqueAppService : AppServiceBase<BarraFisikContext>, IMovimentacaoEstoqueAppService
    {
        private readonly IMovimentacaoEstoqueService _movimentacaoService;
        private readonly IEstoqueService _estoqueService;
        private readonly ILogSistemaService _logSistemaService;

        public MovimentacaoEstoqueAppService(IMovimentacaoEstoqueService movimentacaoService, ILogSistemaService logSistemaService, IEstoqueService estoqueService)
        {
            _movimentacaoService = movimentacaoService;
            _logSistemaService = logSistemaService;
            _estoqueService = estoqueService;
        }


        public void Add(MovimentacaoEstoqueViewModel movimentacaoViewModel)
        {
            var movimentacao = Mapper.Map<MovimentacaoEstoqueViewModel, MovimentacaoEstoque>(movimentacaoViewModel);

            BeginTransaction();
            //Cadastra Movimentacao
            _movimentacaoService.Add(movimentacao);

            _logSistemaService.AddLog("MovimentacaoEstoque", movimentacaoViewModel.MovimentacaoId, "Cadastro", "");
            Commit();
        }

        public MovimentacaoEstoqueViewModel GetById(Guid id)
        {
            return Mapper.Map<MovimentacaoEstoque, MovimentacaoEstoqueViewModel>(_movimentacaoService.GetById(id));
        }

        public IEnumerable<MovimentacaoEstoqueViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<MovimentacaoEstoque>, IEnumerable<MovimentacaoEstoqueViewModel>>(_movimentacaoService.GetAll());
        }

        public IEnumerable<MovimentacaoEstoqueViewModel> GetMovimentacoes()
        {
            return Mapper.Map<IEnumerable<MovimentacaoEstoque>, IEnumerable<MovimentacaoEstoqueViewModel>>(_movimentacaoService.GetMovimentacoes());
        }

        public IEnumerable<MovimentacaoEstoqueViewModel> GetByEstoque(Guid id)
        {
            return Mapper.Map<IEnumerable<MovimentacaoEstoque>, IEnumerable<MovimentacaoEstoqueViewModel>>(_movimentacaoService.GetByEstoque(id));
        }

        public bool Update(MovimentacaoEstoqueViewModel movimentacaoViewModel)
        {
            var movimentacao = Mapper.Map<MovimentacaoEstoqueViewModel, MovimentacaoEstoque>(movimentacaoViewModel);

            var movimentacaoBase = _movimentacaoService.GetById(movimentacao.MovimentacaoId);

            var e = _estoqueService.GetById(movimentacao.EstoqueId);

            BeginTransaction();
            //Estou alterando acrescentando quantidade
            if (movimentacaoBase.Quantidade < movimentacao.Quantidade)
                e.Quantidade = e.Quantidade + (movimentacao.Quantidade - movimentacaoBase.Quantidade);
            else
            {
                //Estou alterando retirando quantidade
                if (movimentacaoBase.Quantidade > movimentacao.Quantidade)
                {
                    //Verifica se tem estoque suficiente para quantidade retirada
                    if (e.Quantidade < (movimentacaoBase.Quantidade - movimentacao.Quantidade))
                        return false;
                    else
                        e.Quantidade = e.Quantidade - (movimentacaoBase.Quantidade - movimentacao.Quantidade);
                }
            }

            e.ValorTotal = e.Quantidade*e.ValorUnitario;
                       
            //Atualiza Movimentacao
            _movimentacaoService.Add(movimentacao);

            //Atualiza estoque
            _estoqueService.Update(e);

            _logSistemaService.AddLog("MovimentacaoEstoque", movimentacaoViewModel.MovimentacaoId, "Update", "");
            Commit();

            return true;
        }

        public bool Remove(Guid id)
        {
            var movimentacao = Mapper.Map<MovimentacaoEstoqueViewModel, MovimentacaoEstoque>(GetById(id));
      
            var e = _estoqueService.GetById(movimentacao.EstoqueId);

            if (e.Quantidade < movimentacao.Quantidade)
            {
                return false;
            }
            else
            {
                BeginTransaction();
                //Delete movimentacao
                _movimentacaoService.Remove(movimentacao);

                //Atualiza quantidade em estoque
                e.Quantidade = e.Quantidade - movimentacao.Quantidade;
                _estoqueService.Update(e);

                _logSistemaService.AddLog("MovimentacaoEstoque", movimentacao.MovimentacaoId, "Remove", "");

                Commit();

                return true;
            }
                
            
        }

        public void Dispose()
        {
            _movimentacaoService.Dispose();
        }
    }
}
