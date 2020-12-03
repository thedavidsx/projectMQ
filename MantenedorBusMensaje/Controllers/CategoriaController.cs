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
    public class CategoriaController : ControllerBase
    {
        static ConectFactory sm = ConectFactory.Instance;

        private static NHibernate.ISession session = sm.Session;

        IEnumerable<NOT_CATEGORIA> listaCategoria;

        CategoriaQuery categoria = new CategoriaQuery(session, null);


        // GET: api/Categoria
        [HttpGet(Name = "GetCategoria")]
        public IEnumerable<NOT_CATEGORIA> GetCategoria()
        {
            try
            {
                listaCategoria = categoria.AllRead();
            }
            catch (Exception e)
            {
                //HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Deleted" };
            }
            finally
            {
                //session.Dispose();
            }
            return listaCategoria;
        }

        // GET: api/Categoria/5
        [HttpGet("{id}", Name = "GetCategoriaById")]
        public IEnumerable<NOT_CATEGORIA> GetCategoriaById(Guid id)
        {
            NOT_CATEGORIA listaCategoria = new NOT_CATEGORIA();

            try
            {
                listaCategoria = categoria.Read(id);
            }
            catch (Exception e)
            {
                //HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Deleted" };
            }
            finally
            {
                //session.Dispose();
            }

            yield return listaCategoria;
        }

        // POST: api/Categoria
        [HttpPost(Name = "PostCategoria")]
        public HttpResponseMessage PostCategoria([FromBody] NOT_CATEGORIA data)
        {
            if (data == null)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "POST body is null" };

            try
            {
                NOT_CATEGORIA rs = categoria.Create(data);

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

        // PUT: api/Categoria/5
        [HttpPut("{id}", Name = "PutCategoria")]
        public HttpResponseMessage PutCategoria(Guid id, [FromBody] NOT_CATEGORIA data)
        {
            if (Guid.Equals(id, null))
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "Identifier could not be empty" };

            if (data == null)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "PUT body is null" };

            try
            {
                categoria.Update(id, data);

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

        // DELETE: api/Categoria/5
        [HttpDelete("{id}", Name = "DeleteCategoria")]
        public HttpResponseMessage DeleteCategoria(Guid id)
        {
            if (Guid.Equals(id, null))
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "Identifier could not be empty" };
            try
            {
                categoria.Delete(id);

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
