using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using SondaMQ.AdministradorModeloMensaje.Model;
using SondaMQ.AdministradorModeloMensaje.QueryInterfases;
using SondaMQ.AdministradorModeloMensaje.Common;

namespace SondaMQ.AdministradorModeloMensaje.Querys
{
    public class PlantillaQuery : QueryInterfases<NOT_PLANTILLA>
    {
        private readonly IDateTime _dateTime;
        private readonly ISession _session;
        public PlantillaQuery(ISession session, IDateTime dateTime)
        {
            _session = session;
            _dateTime = dateTime;
        }
        public NOT_PLANTILLA Create(NOT_PLANTILLA plantilla)
        {
            ITransaction transaction = _session.BeginTransaction();
            //DateTime now = DateTime.Now;
            try
            {
                plantilla.id_not_plantilla = Guid.NewGuid();

                var rs = _session.Save(plantilla);

                transaction.Commit();

                return plantilla;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                transaction.Dispose();
            }
        }
        public void Update(NOT_PLANTILLA plantilla)
        {
            ITransaction transaction = _session.BeginTransaction();

            //PropertyInfo[] properties = typeof(NOT_CANAL).GetProperties();
            try
            {
                _session.Merge(plantilla);

                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                transaction.Dispose();
            }
        }
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
        public NOT_PLANTILLA Read(int id)
        {
            throw new NotImplementedException();
        }
        public NOT_PLANTILLA Read(Guid id)
        {
            try
            {
                var plantilla = _session.Get<NOT_PLANTILLA>(id);
                return plantilla;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<NOT_PLANTILLA> AllRead()
        {
            try
            {
                var plantilla = _session.QueryOver<NOT_PLANTILLA>().List();
                return plantilla;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public NOT_PLANTILLA Read(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(string filtro, NOT_PLANTILLA objeto)
        {
            throw new NotImplementedException();
        }

        public void Update(int filtro, NOT_PLANTILLA objeto)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid filtro, NOT_PLANTILLA objeto)
        {
            throw new NotImplementedException();
        }
    }
}