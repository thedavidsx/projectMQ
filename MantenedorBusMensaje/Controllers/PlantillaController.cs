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
    public class PlantillaController : ControllerBase
    {
        static ConectFactory sm = ConectFactory.Instance;

        private static NHibernate.ISession session = sm.Session;

        IEnumerable<NOT_PLANTILLA> listaPlantilla;

        PlantillaQuery Plantilla = new PlantillaQuery(session, null);

        // GET: api/Plantilla
        [HttpGet(Name = "GetPlantilla")]
        public IEnumerable<NOT_PLANTILLA> GetPlantilla()
        {
            try
            {
                listaPlantilla = Plantilla.AllRead();
            }
            catch (Exception e)
            {
                //HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Deleted" };
            }
            finally
            {
                //session.Dispose();
            }
            return listaPlantilla;
        }

        // GET: api/Plantilla/5
        [HttpGet("{id}", Name = "GetPlantillaById")]
        public IEnumerable<NOT_PLANTILLA> GetPlantillaById(Guid id)
        {
            NOT_PLANTILLA listaPlantilla = new NOT_PLANTILLA();

            try
            {
                listaPlantilla = Plantilla.Read(id);
            }
            catch (Exception e)
            {
                //HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Deleted" };
            }
            finally
            {
                //session.Dispose();
            }

            yield return listaPlantilla;
        }

        // POST: api/Plantilla
        [HttpPost(Name = "PostPlantilla")]
        public HttpResponseMessage PostPlantilla([FromBody] NOT_PLANTILLA data)
        {
            if (data == null)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "POST body is null" };

            try
            {
                NOT_PLANTILLA rs = Plantilla.Create(data);

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

        // PUT: api/Plantilla/5
        [HttpPut("{id}", Name = "PutPlantilla")]
        public HttpResponseMessage PutPlantilla(Guid id, [FromBody] NOT_PLANTILLA data)
        {
            if (Guid.Equals(id, null))
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "Identifier could not be empty" };

            if (data == null)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "PUT body is null" };

            try
            {
                Plantilla.Update(data);

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

        // DELETE: api/Plantilla/5
        [HttpDelete("{id}", Name = "DeletePlantilla")]
        public HttpResponseMessage DeletePlantilla(Guid id)
        {
            if (Guid.Equals(id, null))
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "Identifier could not be empty" };
            try
            {
                Plantilla.Delete(id);

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
