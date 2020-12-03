using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using SondaMQ.AdministradorModeloMensaje.Model;
using SondaMQ.AdministradorModeloMensaje.QueryInterfases;
using SondaMQ.AdministradorModeloMensaje.Common;
using System.Reflection;

namespace SondaMQ.AdministradorModeloMensaje.Querys
{
    public class CanalQuery : QueryInterfases<NOT_CANAL>
    {
        private readonly IDateTime _dateTime;
        private readonly ISession _session;
        public CanalQuery(ISession session, IDateTime dateTime)
        {
            _session = session;
            _dateTime = dateTime;
        }
        public NOT_CANAL Create(NOT_CANAL canal)
        {
            ITransaction transaction = _session.BeginTransaction();
            //DateTime now = DateTime.Now;
            try
            {
                var rs = _session.SaveAsync(canal);

                transaction.Commit();

                return canal;
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
        public void Update(string id , NOT_CANAL canal)
        {
            ITransaction transaction = _session.BeginTransaction();
            
            //PropertyInfo[] properties = typeof(NOT_CANAL).GetProperties();
            try
            {
                _session.Merge(canal);

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

        public void Delete(string id)
        {
            ITransaction transaction = _session.BeginTransaction();
            try
            {
                var canal = _session.Get<NOT_CANAL>(id);

                _session.Delete(canal);

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

        public NOT_CANAL Read(string id)
        {
            try
            {
                var canal = _session.Get<NOT_CANAL>(id);
                return canal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<NOT_CANAL> AllRead()
        {
            try
            {
                var canal = _session.QueryOver<NOT_CANAL>().List();
                return canal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(int filtro, NOT_CANAL objeto)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid filtro, NOT_CANAL objeto)
        {
            throw new NotImplementedException();
        }

        public NOT_CANAL Read(int id)
        {
            throw new NotImplementedException();
        }

        public NOT_CANAL Read(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}