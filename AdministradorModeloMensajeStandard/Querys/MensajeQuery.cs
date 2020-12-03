using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using SondaMQ.AdministradorModeloMensaje.Model;
using SondaMQ.AdministradorModeloMensaje.QueryInterfases;
using SondaMQ.AdministradorModeloMensaje.Common;

namespace SondaMQ.AdministradorModeloMensaje.Querys
{
    public class MensajeQuery : QueryInterfases<NOT_MENSAJE>
    {
        private readonly IDateTime _dateTime;
        private readonly ISession _session;
        public MensajeQuery(ISession session, IDateTime dateTime)
        {
            _session = session;
            _dateTime = dateTime;
        }
        public NOT_MENSAJE Create(NOT_MENSAJE mensaje)
        {
            ITransaction transaction = _session.BeginTransaction();
            //DateTime now = DateTime.Now;
            try
            {
                mensaje.id_not_mensaje = Guid.NewGuid();

                var rs = _session.Save(mensaje);

                transaction.Commit();

                return mensaje;
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
        public void Update(NOT_MENSAJE mensaje)
        {
            ITransaction transaction = _session.BeginTransaction();

            //PropertyInfo[] properties = typeof(NOT_CANAL).GetProperties();
            try
            {
                _session.Merge(mensaje);

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
            ITransaction transaction = _session.BeginTransaction();
            try
            {
                var mensaje = _session.Get<NOT_MENSAJE>(id);

                _session.Delete(mensaje);

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
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
        public NOT_MENSAJE Read(int id)
        {
            throw new NotImplementedException();
        }
        public NOT_MENSAJE Read(Guid id)
        {
            try
            {
                var mensaje = _session.Get<NOT_MENSAJE>(id);
                return mensaje;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<NOT_MENSAJE> AllRead()
        {
            try
            {
                var mensaje = _session.QueryOver<NOT_MENSAJE>().List();
                return mensaje;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public NOT_MENSAJE Read(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(string filtro, NOT_MENSAJE objeto)
        {
            throw new NotImplementedException();
        }

        public void Update(int filtro, NOT_MENSAJE objeto)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid filtro, NOT_MENSAJE objeto)
        {
            throw new NotImplementedException();
        }
    }
}