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
    public class OpcionesUsuarioController : ControllerBase
    {
        static ConectFactory sm = ConectFactory.Instance;

        private static NHibernate.ISession session = sm.Session;

        IEnumerable<NOT_OPCIONES_USUARIO> listaOpcionesUsuario;

        OpcionesUsuarioQuery OpcionesUsuario = new OpcionesUsuarioQuery(session, null);

        // GET: api/OpcionesUsuario
        [HttpGet(Name = "GetOpcionesUsuario")]
        public IEnumerable<NOT_OPCIONES_USUARIO> GetOpcionesUsuario()
        {
            try
            {
                listaOpcionesUsuario = OpcionesUsuario.AllRead();
            }
            catch (Exception e)
            {
                //HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Deleted" };
            }
            finally
            {
                //session.Dispose();
            }
            return listaOpcionesUsuario;
        }

        // GET: api/OpcionesUsuario/5
        [HttpGet("{id}", Name = "GetOpcionesUsuarioById")]
        public IEnumerable<NOT_OPCIONES_USUARIO> GetOpcionesUsuarioById(Guid id)
        {
            NOT_OPCIONES_USUARIO listaOpcionesUsuario = new NOT_OPCIONES_USUARIO();

            try
            {
                listaOpcionesUsuario = OpcionesUsuario.Read(id);
            }
            catch (Exception e)
            {
                //HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Deleted" };
            }
            finally
            {
                //session.Dispose();
            }

            yield return listaOpcionesUsuario;
        }

        // POST: api/OpcionesUsuario
        [HttpPost(Name = "PostOpcionesUsuario")]
        public HttpResponseMessage PostOpcionesUsuario([FromBody] NOT_OPCIONES_USUARIO data)
        {
            if (data == null)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "POST body is null" };

            try
            {
                NOT_OPCIONES_USUARIO rs = OpcionesUsuario.Create(data);

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

        // PUT: api/OpcionesUsuario/5
        [HttpPut("{id}", Name = "PutOpcionesUsuario")]
        public HttpResponseMessage PutOpcionesUsuario(Guid id, [FromBody] NOT_OPCIONES_USUARIO data)
        {
            if (Guid.Equals(id, null))
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "Identifier could not be empty" };

            if (data == null)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "PUT body is null" };

            try
            {
                OpcionesUsuario.Update(data);

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

        // DELETE: api/OpcionesUsuario/5
        [HttpDelete("{id}", Name = "DeleteOpcionesUsuario")]
        public HttpResponseMessage DeleteOpcionesUsuario(Guid id)
        {
            if (Guid.Equals(id, null))
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "Identifier could not be empty" };
            try
            {
                OpcionesUsuario.Delete(id);

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
