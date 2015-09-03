﻿using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;
using BarraFisik.API.Filters;

namespace BarraFisik.API.Controllers
{
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

        [HttpPost]
        [Route("clientes")]
        public HttpResponseMessage Post(ClienteViewModel cliente)
        {
            if (ModelState.IsValid)
            {
                var result = _clienteApp.Add(cliente);

                if (!result.IsValid)
                {
                    foreach (var validationAppError in result.Erros)
                    {
                        ModelState.AddModelError(string.Empty, validationAppError.Message);
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                if (cliente.Foto != null)
                {                    
                    //Convert and Upload image
                    ConvertAndSave(cliente.Foto, cliente.ClienteId);
                    cliente.Path = "/assets/images/fotos/" + cliente.ClienteId + ".jpg";
                    _clienteApp.Update(cliente);
                }

                return Request.CreateResponse(HttpStatusCode.Created, cliente.ClienteId);

            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("clientes")]
        public HttpResponseMessage Put(ClienteViewModel cliente)
        {
            if (ModelState.IsValid)
            {
                var result = _clienteApp.Update(cliente);

                if (!result.IsValid)
                {
                    foreach (var validationAppError in result.Erros)
                    {
                        ModelState.AddModelError(string.Empty, validationAppError.Message);
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                if (cliente.Foto != null)
                {
                    //Convert and Upload image
                    ConvertAndSave(cliente.Foto, cliente.ClienteId);
                    cliente.Path = "/assets/images/fotos/" + cliente.ClienteId + ".jpg";
                    _clienteApp.Update(cliente);
                }

                return Request.CreateResponse(HttpStatusCode.Created, "Cliente Atualizado com Sucesso!");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("cliente/{id:Guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var cliente = _clienteApp.GetById(id);

            if (cliente == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Cliente Não Encontrado");
            }

            return Request.CreateResponse(HttpStatusCode.OK, cliente);            
        }        

        [HttpPut]
        [Route("cliente/desativar/{id:Guid}")]
        public Task<HttpResponseMessage> DesativarCliente(Guid id)
        {
            var cliente = _clienteApp.GetById(id);

            var response = new HttpResponseMessage();

            if (cliente == null)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                cliente.IsAtivo = false;
                _clienteApp.Update(cliente);

                response = Request.CreateResponse(HttpStatusCode.OK, "Cliente Desativado");
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPut]
        [Route("cliente/ativar/{id:Guid}")]
        public Task<HttpResponseMessage> AtivarCliente(Guid id)
        {
            var cliente = _clienteApp.GetById(id);

            var response = new HttpResponseMessage();

            if (cliente == null)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                cliente.IsAtivo = true;
                _clienteApp.Update(cliente);
                response = Request.CreateResponse(HttpStatusCode.OK, "Cliente Desativado");
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
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
            var path = "C:/Users/jefferson/Documents/Visual Studio 2015/Projects/BarraFisik/BarraFisik.UI/assets/images/fotos/";
            var fileName = id + ".jpg";
            var fullPath = Path.Combine(path, fileName);
            image.Save(fullPath);
        }
    }
}