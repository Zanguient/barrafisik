using System;
using System.Collections.Generic;
using AutoMapper;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Infra.Data.Context;
using BarraFisik.Domain.ValueObjects;
using BarraFisik.Application.Validation;

namespace BarraFisik.Application.App
{
    public class ReceitasAppService : AppServiceBase<BarraFisikContext>, IReceitasAppService
    {
        private readonly IReceitasService _receitasService;
        private readonly IClienteService _clienteService;
        private readonly ILogReceitasDespesasService _logReceitasDespesasService;
        private readonly ILogSistemaService _logSistemaService;

        public ReceitasAppService(IReceitasService receitasService, ILogReceitasDespesasService logReceitasDespesasService, IClienteService clienteService, ILogSistemaService logSistemaService)
        {
            _receitasService = receitasService;
            _clienteService = clienteService;
            _logReceitasDespesasService = logReceitasDespesasService;
            _logSistemaService = logSistemaService;
        }

        public void Add(ReceitasViewModel receitasViewModel)
        {
            var receita = Mapper.Map<ReceitasViewModel, Receitas>(receitasViewModel);

            BeginTransaction();
            receita.DataEmissao = DateTime.Now;
            _receitasService.Add(receita);

            ////Log
            _logReceitasDespesasService.AddLog("Cadastro", GetLog(receita));
            Commit();
        }

        public ValidationAppResult AddMensalidade(ReceitasViewModel receitasViewModel)
        {
            var mensalidade = Mapper.Map<ReceitasViewModel, Receitas>(receitasViewModel);

            BeginTransaction();

            var result = _receitasService.AddMensalidade(mensalidade);

            if (!result.IsValid)
                return DomainToApplicationResult(result);

            //Atualiza situacao do cliente caso a mensalidade que esta sendo paga se refere ao mês atual
            var today = DateTime.Now;
            if (receitasViewModel.MesReferencia >= today.Month && receitasViewModel.AnoReferencia >= today.Year && receitasViewModel.DataPagamento != null)
            {
                var cliente = _clienteService.GetByIdMensalidade(receitasViewModel.ClienteId);
                if (cliente.IsAtivo)
                {
                    cliente.Situacao = "Regular";
                    _clienteService.Update(cliente);

                    _logSistemaService.AddLog("Cliente", cliente.ClienteId, "Update", "Alteração da situacao para: REGULAR. Adicionado mensalidade: " + mensalidade.ReceitasId);
                }
            }

            _logReceitasDespesasService.AddLog("Cadastro", GetLog(mensalidade));

            Commit();

            return DomainToApplicationResult(result);          
        }

        public ReceitasViewModel GetById(Guid id)
        {
            return Mapper.Map<Receitas, ReceitasViewModel>(_receitasService.GetById(id));
        }

        public IEnumerable<ReceitasViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Receitas>, IEnumerable<ReceitasViewModel>>(_receitasService.GetAll());
        }

        public IEnumerable<ReceitasViewModel> GetMensalidadesCliente(Guid? idCliente)
        {
            return Mapper.Map<IEnumerable<Receitas>, IEnumerable<ReceitasViewModel>>(_receitasService.GetMensalidadesCliente(idCliente));
        }

        public IEnumerable<ReceitasViewModel> GetAvaliacaoCliente(Guid? idCliente)
        {
            return Mapper.Map<IEnumerable<Receitas>, IEnumerable<ReceitasViewModel>>(_receitasService.GetAvaliacaoCliente(idCliente));
        }

        public IEnumerable<ReceitasViewModel> GetReceitas()
        {
            return Mapper.Map<IEnumerable<Receitas>, IEnumerable<ReceitasViewModel>>(_receitasService.GetReceitas());
        }

        public IEnumerable<ReceitasViewModel> SearchReceitas(SearchReceitasViewModel sr)
        {
            var search = Mapper.Map<SearchReceitasViewModel, SearchReceita>(sr);
            return Mapper.Map<IEnumerable<Receitas>, IEnumerable<ReceitasViewModel>>(_receitasService.SearchReceitas(search));
        }

        public void Update(ReceitasViewModel receitasViewModel)
        {
            var receita = Mapper.Map<ReceitasViewModel, Receitas>(receitasViewModel);

            BeginTransaction();
            _receitasService.Add(receita);

            //Log
            _logReceitasDespesasService.AddLog("Cadastro", GetLog(receita));
            Commit();
        }

        public ValidationAppResult UpdateMensalidade(ReceitasViewModel mensalidadeViewModel)
        {
            var mensalidade = Mapper.Map<ReceitasViewModel, Receitas>(mensalidadeViewModel);

            BeginTransaction();

            var result = _receitasService.AddMensalidade(mensalidade);

            if (!result.IsValid)
                return DomainToApplicationResult(result);

            Commit();


            BeginTransaction();
            //Verifica se existe alguma mensalidade com mes e ano maior ou igual a data atual            
            var today = DateTime.Now;
            bool existeMensalidade = false;
            var cliente = _clienteService.GetByIdMensalidade(mensalidadeViewModel.ClienteId);
            foreach (var mensalidades in GetMensalidadesCliente(mensalidadeViewModel.ClienteId))
            {
                if (mensalidades.MesReferencia >= today.Month && mensalidades.AnoReferencia >= today.Year)
                {
                    existeMensalidade = true;
                }
            }
            if (cliente.IsAtivo)
            {
                if (existeMensalidade && cliente.Situacao != "Regular" && mensalidade.DataPagamento != null)
                {
                    cliente.Situacao = "Regular";
                    _clienteService.Update(cliente);
                    _logSistemaService.AddLog("Cliente", cliente.ClienteId, "Update",
                        "Alteração da situacao para: REGULAR. Atualizado mensalidade: " + mensalidade.ReceitasId);
                }
                else if ((!existeMensalidade && cliente.Situacao != "Pendente"))
                {
                    cliente.Situacao = "Pendente";
                    _clienteService.Update(cliente);
                    _logSistemaService.AddLog("Cliente", cliente.ClienteId, "Update",
                        "Alteração da situacao para: PENDENTE. Atualizado mensalidade: " + mensalidade.ReceitasId);
                } else if(existeMensalidade && mensalidade.DataPagamento == null) //Existe mensalidade mas não existe data de pagamento (não está quitado) - status para pendente
                {
                    cliente.Situacao = "Pendente";
                    _clienteService.Update(cliente);
                    _logSistemaService.AddLog("Cliente", cliente.ClienteId, "Update",
                        "Alteração da situacao para: PENDENTE. Atualizado mensalidade: " + mensalidade.ReceitasId);
                }
            }

            _logReceitasDespesasService.AddLog("Cadastro", GetLog(mensalidade));
            Commit();

            return DomainToApplicationResult(result);
        }

        public void Remove(Guid id)
        {
            var receita = Mapper.Map<ReceitasViewModel, Receitas>(GetById(id));

            BeginTransaction();
            _receitasService.Remove(receita);

            //Log
            _logReceitasDespesasService.AddLog("Remove", GetLog(receita));
            Commit();
        }

        public void RemoveMensalidade(Guid id)
        {
            var mensalidade = Mapper.Map<ReceitasViewModel, Receitas>(GetById(id));
            var idCliente = mensalidade.ClienteId;

            BeginTransaction();
            _receitasService.Remove(mensalidade);
            Commit();

            BeginTransaction();
            //Verifica se existe alguma mensalidade com mes e ano maior ou igual a data atual            
            var today = DateTime.Now;
            bool existeMensalidade = false;
            var cliente = _clienteService.GetByIdMensalidade(idCliente);
            foreach (var mensalidades in GetMensalidadesCliente(idCliente))
            {
                if (mensalidades.MesReferencia >= today.Month && mensalidades.AnoReferencia >= today.Year)
                {
                    existeMensalidade = true;
                }
            }

            if (existeMensalidade && cliente.Situacao != "Regular" && mensalidade.DataPagamento != null)
            {
                cliente.Situacao = "Regular";
                _clienteService.Update(cliente);
                _logSistemaService.AddLog("Cliente", cliente.ClienteId, "Update", "Alteração da situacao para: REGULAR. Deletado mensalidade: " + mensalidade.ReceitasId);
            }
            else if ((!existeMensalidade && cliente.Situacao != "Pendente"))
            {
                cliente.Situacao = "Pendente";
                _clienteService.Update(cliente);
                _logSistemaService.AddLog("Cliente", cliente.ClienteId, "Update", "Alteração da situacao para: PENDENTE. Deletado mensalidade: " + mensalidade.ReceitasId);
            }

            _logReceitasDespesasService.AddLog("Remove", GetLog(mensalidade));
            Commit();
        }

        public void Dispose()
        {
            _receitasService.Dispose();
        }

        private static LogReceitasDespesas GetLog(Receitas r)
        {
            var logRecDesp = new LogReceitasDespesas
            {
                Documento = r.Documento,
                DataVencimento = r.DataVencimento,
                DataPagamento = r.DataPagamento,
                DataEmissao = r.DataEmissao,
                Valor = r.Valor,
                Juros = r.Juros,
                Multa = r.Multa,
                ValorTotal = r.ValorTotal,
                Observacao = r.Observacao,
                Situacao = r.Situacao,
                Tipo = "Receita",
                CategoriaFinanceiraId = r.CategoriaFinanceiraId.ToString(),
                FornecedorId = r.ClienteId.ToString(),
                FuncionarioId = r.FuncionarioId.ToString(),
                TipoPagamentoId = r.TipoPagamentoId,
                RegistroId = r.ReceitasId.ToString()
            };

            return logRecDesp;
        }        
    }
}