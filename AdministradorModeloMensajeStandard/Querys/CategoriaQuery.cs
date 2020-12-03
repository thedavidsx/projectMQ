using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using SondaMQ.AdministradorModeloMensaje.Model;
using SondaMQ.AdministradorModeloMensaje.QueryInterfases;
using SondaMQ.AdministradorModeloMensaje.Common;

namespace SondaMQ.AdministradorModeloMensaje.Querys
{
    public class CategoriaQuery : QueryInterfases<NOT_CATEGORIA>
    {
        private readonly IDateTime _dateTime;
        private readonly ISession _session;
        public CategoriaQuery(ISession session, IDateTime dateTime)
        {
            _session = session;
            _dateTime = dateTime;
        }
        public NOT_CATEGORIA Create(NOT_CATEGORIA categoria)
        {
            ITransaction transaction = _session.BeginTransaction();
            //DateTime now = DateTime.Now;
            try
            {
                categoria.id_not_categoria = Guid.NewGuid();

                var rs = _session.Save(categoria);

                transaction.Commit();

                return categoria;
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
        public void Update(NOT_CATEGORIA categoria)
        {
            ITransaction transaction = _session.BeginTransaction();

            //PropertyInfo[] properties = typeof(NOT_CANAL).GetProperties();
            try
            {
                _session.Merge(categoria);

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
                var categoria = _session.Get<NOT_CATEGORIA>(id);

                _session.Delete(categoria);

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
        public NOT_CATEGORIA Read(int id)
        {
            throw new NotImplementedException();
        }
        public NOT_CATEGORIA Read(Guid id)
        {
            try
            {
                var categoria = _session.Get<NOT_CATEGORIA>(id);
                return categoria;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<NOT_CATEGORIA> AllRead()
        {
            try
            {
                var categoria = _session.QueryOver<NOT_CATEGORIA>().List();
                return categoria;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public NOT_CATEGORIA Read(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(string filtro, NOT_CATEGORIA objeto)
        {
            throw new NotImplementedException();
        }

        public void Update(int filtro, NOT_CATEGORIA objeto)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid filtro, NOT_CATEGORIA objeto)
        {
            throw new NotImplementedException();
        }
    }
}