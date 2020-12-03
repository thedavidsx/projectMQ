using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using SondaMQ.AdministradorModeloMensaje.Conect;
using SondaMQ.AdministradorModeloMensaje.Model;
using SondaMQ.AdministradorModeloMensaje.Querys;

namespace SondaMQ.MantenedorBusMensaje.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CanalController : ControllerBase
    {
        static ConectFactory sm = ConectFactory.Instance;

        private static NHibernate.ISession session = sm.Session;

        IEnumerable<NOT_CANAL> listaCanal;

        CanalQuery canal = new CanalQuery(session, null);

        // GET: api/Canal
        [HttpGet(Name = "GetCanal")]
        public IEnumerable<NOT_CANAL> GetCanal()
        {
            try
            {
                listaCanal = canal.AllRead();
            }
            catch (Exception e)
            {
                //HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Deleted" };
            }
            finally
            {
                //session.Dispose();
            }
            return listaCanal;
        }

        // GET: api/Canal/5
        [HttpGet("{id}", Name = "GetCanalById")]
        public IEnumerable<NOT_CANAL> GetCanalById(String id)
        {
            NOT_CANAL listaCanal = new NOT_CANAL();

            try
            {
                listaCanal = canal.Read(id);   
            }
            catch (Exception e)
            {
                //HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Deleted" };
            }
            finally
            {
                //session.Dispose();
            }
            
            yield return listaCanal;
        }

        // POST: api/Canal
        [HttpPost(Name = "PostCanal")]
        public HttpResponseMessage PostCanal([FromBody] NOT_CANAL data)
        {
            if (data == null)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "POST body is null" };

            try
            {
           
                NOT_CANAL rs = canal.Create(data);

                return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Saved"};
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

        public object parse(object a)
        {
            return a;
        }

        // PUT: api/Canal/5
        [HttpPut("{id}", Name = "PutCanal")]
        public HttpResponseMessage PutCanal(string id, [FromBody]  NOT_CANAL data)
        {
            if (String.IsNullOrEmpty(id))
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "Identifier could not be empty" };

            if (data == null)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "PUT body is null" };

            try
            {
                canal.Update(id, data);

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
        [HttpDelete("{id}", Name = "DeleteCanal")]
        public HttpResponseMessage Delete(string id)
        {
            if (String.IsNullOrEmpty(id))
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "Identifier could not be empty" };
            try
            {
                canal.Delete(id);

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
