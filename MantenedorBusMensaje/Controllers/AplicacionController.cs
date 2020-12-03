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
    public class AplicacionController : ControllerBase
    {
        static ConectFactory sm = ConectFactory.Instance;

        private static NHibernate.ISession session = sm.Session;

        IEnumerable<NOT_APLICACION> listaAplicacion;

        AplicacionQuery aplicacion = new AplicacionQuery(session, null);

        // GET: api/Aplicacion
        [HttpGet(Name = "GetAplicacion")]
        public IEnumerable<NOT_APLICACION> GetAplicacion()
        {
            try
            {
                listaAplicacion = aplicacion.AllRead();
            }
            catch (Exception e)
            {
                //HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Deleted" };
            }
            finally
            {
                //session.Dispose();
            }
            return listaAplicacion;
        }

        // GET: api/Aplicacion/5
        [HttpGet("{id}", Name = "GetAplicacionById")]
        public IEnumerable<NOT_APLICACION> GetAplicacionById(Guid id)
        {
            NOT_APLICACION listaAplicacion = new NOT_APLICACION();

            try
            {
                listaAplicacion = aplicacion.Read(id);
            }
            catch (Exception e)
            {
                //HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Deleted" };
            }
            finally
            {
                //session.Dispose();
            }

            yield return listaAplicacion;
        }

        // POST: api/Aplicacion
        [HttpPost(Name = "PostAplicacion")]
        
        public HttpResponseMessage PostAplicacion([FromBody] NOT_APLICACION data)
        {
            if (data == null)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "POST body is null" };


            try
            {
                NOT_APLICACION rs = aplicacion.Create(data);

                return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = rs.id_not_aplicacion.ToString() };
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

        // PUT: api/Aplicacion/5
        [HttpPut("{id}", Name = "PutAplicacion")]
        public HttpResponseMessage PutAplicacion(Guid id, [FromBody] NOT_APLICACION data)
        {
            if (Guid.Equals(id,null))
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "Identifier could not be empty" };

            if (data == null)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "PUT body is null" };

            try
            {
                aplicacion.Update(id, data);

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
        [HttpDelete("{id}", Name = "DeleteAplicacion")]
        public HttpResponseMessage DeleteAplicacion(Guid id)
        {
            if (Guid.Equals(id, null))
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "Identifier could not be empty" };
            try
            {
                aplicacion.Delete(id);

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
