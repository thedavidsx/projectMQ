using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using SondaMQ.AdministradorModeloMensaje.Model;
using SondaMQ.AdministradorModeloMensaje.QueryInterfases;
using SondaMQ.AdministradorModeloMensaje.Common;


namespace SondaMQ.AdministradorModeloMensaje.Querys
{
    public class AplicacionQuery : QueryInterfases<NOT_APLICACION>
    {
        private readonly IDateTime _dateTime;
        private readonly ISession _session;
        public AplicacionQuery(ISession session, IDateTime dateTime)
        {
            _session = session;
            _dateTime = dateTime;
        }

        public IEnumerable<NOT_APLICACION> AllRead()
        {
            try
            {
                var aplicacion = _session.QueryOver<NOT_APLICACION>().List();
                return aplicacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public NOT_APLICACION Create(NOT_APLICACION aplicacion)
        {
            ITransaction transaction = _session.BeginTransaction();
            //DateTime now = DateTime.Now;
            try
            {
                aplicacion.id_not_aplicacion = Guid.NewGuid();

                var rs = _session.Save(aplicacion);

                transaction.Commit();

                return aplicacion;
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
                var aplicacion = _session.Get<NOT_APLICACION>(id);

                _session.Delete(aplicacion);

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

        public NOT_APLICACION Read(Guid id)
        {
            try
            {
                var aplicacion = _session.Get<NOT_APLICACION>(id);
                return aplicacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public NOT_APLICACION Read(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NOT_APLICACION> Read(string id)
        {

            try
            {
                 var aplicacion = _session.QueryOver<NOT_APLICACION>()
                                 .Where(x => x.codigo == id)
                                 .OrderBy(x => x.codigo)
                                 .Asc.List()
                                 .ToList();
                return aplicacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(Guid filtro, NOT_APLICACION aplicacion)
        {
            ITransaction transaction = _session.BeginTransaction();

            //PropertyInfo[] properties = typeof(NOT_CANAL).GetProperties();
            try
            {
                _session.Merge(aplicacion);

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

        public void Update(string filtro, NOT_APLICACION objeto)
        {
            throw new NotImplementedException();
        }

        public void Update(int filtro, NOT_APLICACION objeto)
        {
            throw new NotImplementedException();
        }

        NOT_APLICACION QueryInterfases<NOT_APLICACION>.Read(string id)
        {
            throw new NotImplementedException();
        }
    }
}