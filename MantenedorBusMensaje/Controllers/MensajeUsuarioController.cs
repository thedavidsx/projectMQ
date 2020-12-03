using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SondaMQ.AdministradorModeloMensaje.Conect;
using SondaMQ.AdministradorModeloMensaje.Model;
using SondaMQ.AdministradorModeloMensaje.Querys;

namespace MantenedorBusMensaje.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MensajeUsuarioController : ControllerBase
    {
        static ConectFactory sm = ConectFactory.Instance;

        private static NHibernate.ISession session = sm.Session;

        IEnumerable<NOT_MENSAJE_USUARIO> listaMensajeUsuario;

        MensajeUsuarioQuery MensajeUsuario = new MensajeUsuarioQuery(session, null);

        // GET: api/MensajeUsuario
        [HttpGet(Name = "GetMensajeUsuario")]
        public IEnumerable<NOT_MENSAJE_USUARIO> GetMensajeUsuario()
        {
            try
            {
                listaMensajeUsuario = MensajeUsuario.AllRead();
            }
            catch (Exception e)
            {
                //HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Deleted" };
            }
            finally
            {
                //session.Dispose();
            }
            return listaMensajeUsuario;
        }

        // GET: api/MensajeUsuario/5
        [HttpGet("{id}", Name = "GetMensajeUsuarioById")]
        public IEnumerable<NOT_MENSAJE_USUARIO> GetMensajeUsuarioById(Guid id)
        {
            NOT_MENSAJE_USUARIO listaMensajeUsuario = new NOT_MENSAJE_USUARIO();

            try
            {
                listaMensajeUsuario = MensajeUsuario.Read(id);
            }
            catch (Exception e)
            {
                //HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Deleted" };
            }
            finally
            {
                //session.Dispose();
            }

            yield return listaMensajeUsuario;
        }

        // POST: api/MensajeUsuario
        [HttpPost(Name = "PostMensajeUsuario")]
        public HttpResponseMessage PostMensajeUsuario([FromBody] NOT_MENSAJE_USUARIO data)
        {
            if (data == null)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "POST body is null" };

            try
            {
                NOT_MENSAJE_USUARIO rs = MensajeUsuario.Create(data);

                return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Saved" };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError, ReasonPhrase = $"Document could not be created: {ex.InnerException}" };
            }
            finally
            {
                //session.Dispose();
            }
        }

        // PUT: api/MensajeUsuario/5
        [HttpPut("{id}", Name = "PutMensajeUsuario")]
        public HttpResponseMessage PutMensajeUsuario(Guid id, [FromBody] NOT_MENSAJE_USUARIO data)
        {
            if (Guid.Equals(id, null))
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "Identifier could not be empty" };

            if (data == null)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "PUT body is null" };

            try
            {
                MensajeUsuario.Update(data);

                return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Updated" };

            }
            catch (Exception ex)
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError, ReasonPhrase = $"Document could not be updated: {ex.InnerException}" };
            }
            finally
            {
                //session.Dispose();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}", Name = "DeleteMensajeUsuario")]
        public HttpResponseMessage DeleteMensajeUsuario(Guid id)
        {
            if (Guid.Equals(id, null))
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "Identifier could not be empty" };
            try
            {
                MensajeUsuario.Delete(id);

                return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Deleted" };

            }
            catch (Exception ex)
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError, ReasonPhrase = $"Document could not be Deleted: {ex.InnerException}" };
            }
            finally
            {
                //session.Dispose();
            }
        }
    }
}
