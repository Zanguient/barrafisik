using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    public class ClienteController : ApiController
    {
        private readonly IClienteAppService _clienteApp;
        private readonly IHorarioAppService _horarioApp;

        public ClienteController(IClienteAppService clienteApp, IHorarioAppService horarioApp)
        {
            _clienteApp = clienteApp;
            _horarioApp = horarioApp;
        }

        [HttpGet]
        [Route("clientes")]
        [GzipCompression]
        public async Task<HttpResponseMessage> GetClientes()
        {
            var result = _clienteApp.GetAll();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        [Route("cliente/perfil/{id:Guid}")]
        public async Task<HttpResponseMessage> GetClientePerfil(Guid id)
        {
            var result = _clienteApp.GetClientePerfil(id);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        [Route("clientes/all")]
        [GzipCompression]
        public async Task<HttpResponseMessage> GetClientesAll()
        {
            var result = _clienteApp.GetClientesAll();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        [Route("clientes/{situacao}")]
        [GzipCompression]
        public async Task<HttpResponseMessage> GetClientesSituacao(string situacao)
        {
            var result = _clienteApp.GetClientesSituacao(situacao);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        [Route("aniversariantes/{mes:int}")]
        [GzipCompression]
        public async Task<HttpResponseMessage> GetAniversariantes(int mes)
        {
            var result = _clienteApp.GetAniversariantes(mes);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("clientes")]
        public HttpResponseMessage Post(ClienteHorarioViewModel clienteHorario)
        {
            if (ModelState.IsValid)
            {
                if (clienteHorario.IsAtivo)
                {
                    if (clienteHorario.HSegunda == null && clienteHorario.HTerca == null && clienteHorario.HQuarta == null &&
                        clienteHorario.HQuinta == null && clienteHorario.HSexta == null)
                    {
                        ModelState.AddModelError(string.Empty, "Informe o horário de treino");
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                }

                var result = _clienteApp.Add(clienteHorario);                

                if (!result.IsValid)
                {
                    foreach (var validationAppError in result.Erros)
                    {
                        ModelState.AddModelError(string.Empty, validationAppError.Message);
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                if (clienteHorario.Foto != null)
                {
                    //Convert and Upload image
                    ConvertAndSave(clienteHorario.Foto, clienteHorario.ClienteId);
                    clienteHorario.Path = "/assets/images/fotos/" + clienteHorario.ClienteId + ".jpg";
                    _clienteApp.Update(clienteHorario);
                }

                return Request.CreateResponse(HttpStatusCode.Created, clienteHorario.ClienteId);
            }

            

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("clienteUpdate")]
        public HttpResponseMessage ClienteUpdate(ClienteHorarioViewModel clienteHorario)
        {
            if (ModelState.IsValid)
            {
                if (clienteHorario.IsAtivo)
                {
                    if (clienteHorario.HSegunda == null && clienteHorario.HTerca == null && clienteHorario.HQuarta == null &&
                        clienteHorario.HQuinta == null && clienteHorario.HSexta == null)
                    {
                        ModelState.AddModelError(string.Empty, "Informe o horário de treino");
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                }

                var result = _clienteApp.Update(clienteHorario);

                if (!result.IsValid)
                {
                    foreach (var validationAppError in result.Erros)
                    {
                        ModelState.AddModelError(string.Empty, validationAppError.Message);
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                if (clienteHorario.Path == null)
                {
                    //Remove image from Disk
                    //var path = "C:/Users/jefferson/Documents/Visual Studio 2015/Projects/BarraFisik/BarraFisik.UI/assets/images/fotos/";
                    var path = "C:/SisBarraFisik/BarraFisikUI/assets/images/fotos/";
                    var fileName = clienteHorario.ClienteId + ".jpg";
                    var fullPath = Path.Combine(path, fileName);
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }
                }

                if (clienteHorario.Foto != null)
                {
                    //Convert and Upload image
                    ConvertAndSave(clienteHorario.Foto, clienteHorario.ClienteId);
                    clienteHorario.Path = "/assets/images/fotos/" + clienteHorario.ClienteId + ".jpg";
                    _clienteApp.Update(clienteHorario);
                }

                return Request.CreateResponse(HttpStatusCode.Created, "Cliente Atualizado com Sucesso!");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("cliente/{id:Guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var cliente = _clienteApp.GetByClienteId(id);

            if (cliente == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Cliente Não Encontrado");
            }

            return Request.CreateResponse(HttpStatusCode.OK, cliente);
        }

        [HttpGet]
        [Route("clientes/updateClientesPendentes/{mes:int}/{ano:int}")]
        public HttpResponseMessage UpdateClientesPendentes(int mes, int ano)
        {
            _clienteApp.UpdateClientesPendentes(mes, ano);

            return Request.CreateResponse(HttpStatusCode.OK, "Clientes Atualizados");            
        }

        [HttpPost]
        [Route("clientes/inativarClientes")]
        public HttpResponseMessage InativarClientes(IEnumerable<ClienteViewModel> listClientes)
        {
            _clienteApp.InativarClientes(listClientes);

            return Request.CreateResponse(HttpStatusCode.OK, "Clientes Inativados");
        }

        [HttpGet]
        [Route("clientes/inscritos/{ano:int}")]
        public HttpResponseMessage GetInscritos(int ano)
        {
            var result = _clienteApp.GetTotalInscritos(ano);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }     

        public void ConvertAndSave(byte[] imageBytes, Guid id)
        {
            // Convert Base64 String to byte[]
            //byte[] imageBytes = Convert.FromBase64String(base64String);
            var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            var image = Image.FromStream(ms, true);

            //Save image to Disk
            var path = "C:/SisBarraFisik/BarraFisik.UI/assets/images/fotos/";
            var fileName = id + ".jpg";
            var fullPath = Path.Combine(path, fileName);
            image.Save(fullPath);
        }
    }
}