﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;
using BarraFisik.API.Filters;
using WebApi.OutputCache.V2;

namespace BarraFisik.API.Controllers
{
    [Authorize]
    [RoutePrefix("api")]
    public class ReceitasController : ApiController
    {
        private readonly IReceitasAppService _receitasApp;
        private readonly ICategoriaFinanceiraAppService _categoriaFinanceiraApp;

        public ReceitasController(IReceitasAppService receitasApp, ICategoriaFinanceiraAppService categoriaFinanceiraApp)
        {
            _receitasApp = receitasApp;
            _categoriaFinanceiraApp = categoriaFinanceiraApp;
        }

        [HttpGet]
        [Route("receitas")]
        [GzipCompression]
        public async Task<HttpResponseMessage> Get()
        {
            var result = _receitasApp.GetReceitas();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        [Route("receitas/mensalidades/{id:Guid}")]        
        public async Task<HttpResponseMessage> GetMensalidadesCliente(Guid id)
        {
            var result = _receitasApp.GetMensalidadesCliente(id);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        [Route("receitas/avaliacaofisica/{id:Guid}")]
        public async Task<HttpResponseMessage> GetAvaliacaofisicaCliente(Guid id)
        {
            var result = _receitasApp.GetAvaliacaoCliente(id);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("receitas")]
        public HttpResponseMessage Post(ReceitasViewModel receitasViewModel)
        {
            if (ModelState.IsValid)
            {
                _receitasApp.Add(receitasViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, receitasViewModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPost]
        [Route("receitas/mensalidade")]
        [InvalidateCacheOutput("GetClientes", typeof(ClienteController))]
        public HttpResponseMessage PostMensalidade(ReceitasViewModel receitasViewModel)
        {
            var cache = Configuration.CacheOutputConfiguration().GetCacheOutputProvider(Request);
            cache.RemoveStartsWith(Configuration.CacheOutputConfiguration().MakeBaseCachekey((ClienteController c) => c.GetClientes()));

            DateTime today = DateTime.Today;

            receitasViewModel.SubCategoriaFinanceiraId = new Guid("0d57c87d-3bd9-420b-ab98-123fdb75a269");
            receitasViewModel.CategoriaFinanceiraId = new Guid("1c1278df-f5a5-4407-a0c4-bdbb71c362b1");
            receitasViewModel.DataEmissao = DateTime.Now; 
            receitasViewModel.DataVencimento = new DateTime(today.Year, today.Month, 10);
            if (ModelState.IsValid)
            {
                var result = _receitasApp.AddMensalidade(receitasViewModel);

                if (!result.IsValid)
                {
                    foreach (var validationAppError in result.Erros)
                    {
                        ModelState.AddModelError(string.Empty, validationAppError.Message);
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return Request.CreateResponse(HttpStatusCode.Created, receitasViewModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPost]
        [Route("receitas/avaliacaofisica")]
        public HttpResponseMessage PostAvaliacaoFisica(ReceitasViewModel receitasViewModel)
        {
            receitasViewModel.SubCategoriaFinanceiraId = new Guid("ecaac024-15bd-4ee0-8422-07d809bb1be9");
            receitasViewModel.CategoriaFinanceiraId = new Guid("1c1278df-f5a5-4407-a0c4-bdbb71c362b1");
            if (ModelState.IsValid)
            {
                _receitasApp.Add(receitasViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, receitasViewModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("receitas")]
        public HttpResponseMessage Put(ReceitasViewModel receitasViewModel)
        {
            if (ModelState.IsValid)
            {
                _receitasApp.Update(receitasViewModel);
                return Request.CreateResponse(HttpStatusCode.Created, "Update efetuado com sucesso!");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("receitas/mensalidade")]
        [InvalidateCacheOutput("GetClientes", typeof(ClienteController))]
        public HttpResponseMessage PutMensalidade(ReceitasViewModel receita)
        {
            var cache = Configuration.CacheOutputConfiguration().GetCacheOutputProvider(Request);
            cache.RemoveStartsWith(Configuration.CacheOutputConfiguration().MakeBaseCachekey((ClienteController c) => c.GetClientes()));

            if (ModelState.IsValid)
            {
                var result = _receitasApp.UpdateMensalidade(receita);

                if (!result.IsValid)
                {
                    foreach (var validationAppError in result.Erros)
                    {
                        ModelState.AddModelError(string.Empty, validationAppError.Message);
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return Request.CreateResponse(HttpStatusCode.Created, receita);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("receitas/{id:Guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var receita = _receitasApp.GetById(id);

            if (receita == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Receita Não Encontrada");
            }

            return Request.CreateResponse(HttpStatusCode.OK, receita);
        }

        [HttpDelete]
        [Route("receitas/{id:Guid}")]
        public HttpResponseMessage Remove(Guid id)
        {
            _receitasApp.Remove(id);

            return Request.CreateResponse(HttpStatusCode.OK, "Dado excluído com sucesso!");
        }

        [HttpDelete]
        [Route("receitas/mensalidade/{id:Guid}")]
        [InvalidateCacheOutput("GetClientes", typeof(ClienteController))]
        public HttpResponseMessage RemoveMensalidade(Guid id)
        {
            var cache = Configuration.CacheOutputConfiguration().GetCacheOutputProvider(Request);
            cache.RemoveStartsWith(Configuration.CacheOutputConfiguration().MakeBaseCachekey((ClienteController c) => c.GetClientes()));

            _receitasApp.RemoveMensalidade(id);

            return Request.CreateResponse(HttpStatusCode.OK, "Dado excluído com sucesso!");
        }

        [HttpPost]
        [Route("receitas/search")]
        public async Task<HttpResponseMessage> Search(SearchReceitasViewModel searchViewModel)
        {
            if (searchViewModel == null)
            {
                var result = _receitasApp.GetReceitas();
                var response = Request.CreateResponse(HttpStatusCode.OK, result);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return await tsc.Task;
            }
            else
            {
                var result = _receitasApp.SearchReceitas(searchViewModel);
                var response = Request.CreateResponse(HttpStatusCode.OK, result);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return await tsc.Task;
            }
        }
    }
}