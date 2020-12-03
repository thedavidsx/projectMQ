using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using SondaMQ.AdministradorModeloMensaje.Model;
using SondaMQ.AdministradorModeloMensaje.QueryInterfases;
using SondaMQ.AdministradorModeloMensaje.Common;

namespace SondaMQ.AdministradorModeloMensaje.Querys
{
    public class MensajeUsuarioQuery : QueryInterfases<NOT_MENSAJE_USUARIO>
    {
        private readonly IDateTime _dateTime;
        private readonly ISession _session;
        public MensajeUsuarioQuery(ISession session, IDateTime dateTime)
        {
            _session = session;
            _dateTime = dateTime;
        }
        public NOT_MENSAJE_USUARIO Create(NOT_MENSAJE_USUARIO mensajeUsuario)
        {
            ITransaction transaction = _session.BeginTransaction();
            //DateTime now = DateTime.Now;
            try
            {
                mensajeUsuario.id_not_mensaje_usuario = Guid.NewGuid();

                var rs = _session.Save(mensajeUsuario);

                transaction.Commit();

                return mensajeUsuario;
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
        public void Update(NOT_MENSAJE_USUARIO mensajeUsuario)
        {
            ITransaction transaction = _session.BeginTransaction();

            //PropertyInfo[] properties = typeof(NOT_CANAL).GetProperties();
            try
            {
                _session.Merge(mensajeUsuario);

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
                var mensajeUsuario = _session.Get<NOT_MENSAJE_USUARIO>(id);

                _session.Delete(mensajeUsuario);

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
        public NOT_MENSAJE_USUARIO Read(int id)
        {
            throw new NotImplementedException();
        }
        public NOT_MENSAJE_USUARIO Read(Guid id)
        {
            try
            {
                var mensajeUsuario = _session.Get<NOT_MENSAJE_USUARIO>(id);
                return mensajeUsuario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<NOT_MENSAJE_USUARIO> AllRead()
        {
            try
            {
                var mensajeUsuario = _session.QueryOver<NOT_MENSAJE_USUARIO>().List();
                return mensajeUsuario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public NOT_MENSAJE_USUARIO Read(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(string filtro, NOT_MENSAJE_USUARIO objeto)
        {
            throw new NotImplementedException();
        }

        public void Update(int filtro, NOT_MENSAJE_USUARIO objeto)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid filtro, NOT_MENSAJE_USUARIO objeto)
        {
            throw new NotImplementedException();
        }
    }
}