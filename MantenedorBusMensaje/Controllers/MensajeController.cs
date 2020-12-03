using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SondaMQ.AdministradorModeloMensaje.Conect;
using SondaMQ.AdministradorModeloMensaje.Model;
using SondaMQ.AdministradorModeloMensaje.Querys;
using SondaMQ.ProveedorNotificacionBus.Modelo;

namespace MantenedorBusMensaje.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MensajeController : ControllerBase
    {
        //static ConectFactory sm = ConectFactory.Instance;

        //private static NHibernate.ISession session = sm.Session;

        //IEnumerable< NOT_MENSAJE> listaMensaje;


        //MensajeQuery mensaje = new MensajeQuery(session, null);

        // GET: api/Mensaje
        //[HttpGet(Name = "GetMensaje")]
        //public IEnumerable<NOT_MENSAJE> GetMensaje()
        //{
        //    try
        //    {
        //        listaMensaje = mensaje.AllRead();
        //    }
        //    catch (Exception e)
        //    {
        //        //HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Deleted" };
        //    }
        //    finally
        //    {
        //        //session.Dispose();
        //    }
        //    return listaMensaje;
        //}

        // GET: api/Mensaje/5
        //[HttpGet("{id}", Name = "GetMensajeById")]
        //public IEnumerable<NOT_MENSAJE> GetMensajeById(Guid id)
        //{
        //    NOT_MENSAJE listaMensaje = new NOT_MENSAJE();

        //    try
        //    {
        //        listaMensaje = mensaje.Read(id);
        //    }
        //    catch (Exception e)
        //    {
        //        //HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Deleted" };
        //    }
        //    finally
        //    {
        //        //session.Dispose();
        //    }

        //    yield return listaMensaje;
        //}

        //// POST: api/Mensaje
        //[HttpPost(Name = "PostMensaje")]
        //public HttpResponseMessage PostMensaje([FromBody] NOT_MENSAJE data)
        //{
        //    if (data == null)
        //        return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "POST body is null" };

        //    try
        //    {
        //        NOT_MENSAJE rs = mensaje.Create(data);

        //        return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Saved" };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError, ReasonPhrase = $"Document could not be created: {ex.InnerException}" };
        //    }
        //    finally
        //    {
        //        //session.Dispose();
        //    }
        //}

        //// PUT: api/Mensaje/5
        //[HttpPut("{id}", Name = "PutMensaje")]
        //public HttpResponseMessage PutMensaje(Guid id, [FromBody] NOT_MENSAJE data)
        //{
        //    if (Guid.Equals(id, null))
        //        return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "Identifier could not be empty" };

        //    if (data == null)
        //        return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "PUT body is null" };

        //    try
        //    {
        //        mensaje.Update(id, data);

        //        return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Updated" };

        //    }
        //    catch (Exception ex)
        //    {
        //        return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError, ReasonPhrase = $"Document could not be updated: {ex.InnerException}" };
        //    }
        //    finally
        //    {
        //        //session.Dispose();
        //    }
        //}

        //// DELETE: api/Mensaje/5
        //[HttpDelete("{id}", Name = "DeleteMensaje")]
        //public HttpResponseMessage DeleteCategoria(Guid id)
        //{
        //    if (Guid.Equals(id, null))
        //        return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "Identifier could not be empty" };
        //    try
        //    {
        //        mensaje.Delete(id);

        //        return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Deleted" };

        //    }
        //    catch (Exception ex)
        //    {
        //        return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError, ReasonPhrase = $"Document could not be Deleted: {ex.InnerException}" };
        //    }
        //    finally
        //    {
        //        //session.Dispose();
        //    }
        //}

        // POST: api/EnviarMensaje
        [Route("EnviarMensaje")]
        [HttpGet(Name = "EnviarMensaje")]
        public async Task<HttpResponseMessage> EnviarMensajeAsync()
        {
            //if (data == null)
            //    return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "POST body is null" };

            try
            {
                List<MensajeUsuarioModel> us = new List<MensajeUsuarioModel>{
                    new MensajeUsuarioModel{
                        id = "DAVID",
                        nombre = "DAVID",
                        email = "david.rodriguezl@sonda.com",
                        canal = "PUSH",
                    },
                };

                MensajeProveedorModel people =  new MensajeProveedorModel{                 
                    cod_aplicacion = "CP",
                    desitinatarios = "",
                    formato = "JSON",
                    listausuarios = us,
                    asunto = "Prueba de Mensaje con servicio",
                    mensaje  = "Mensaje de prueba a ver si llegaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                    prioridad  = 1
                };
                

                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var url = "http://localhost/ProveedorNotificacionBus/SendMessage";
                var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url);

                webrequest.Method = WebRequestMethods.Http.Post;
                webrequest.ContentType = "application/json; charset=UTF-8";


                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };

                var SendData = JsonConvert.SerializeObject(people, microsoftDateFormatSettings);
                var byteData = UTF8Encoding.UTF8.GetBytes(SendData.ToString());

                Stream postStream = null;
                Stream response = null;
                StreamReader reader = null; 

                postStream = webrequest.GetRequestStream();


                postStream.Write(byteData, 0, byteData.Length);

                

                response = webrequest.GetResponse().GetResponseStream();
                reader = new StreamReader(response);


                return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Sended" };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError, ReasonPhrase = $"Message could not be Sended: {ex.Message}" };
            }
            finally
            {
                //session.Dispose();
            }
        }
    }
}
