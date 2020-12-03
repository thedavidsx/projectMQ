using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using SondaMQ.AdministradorModeloMensaje.Model;
using SondaMQ.AdministradorModeloMensaje.QueryInterfases;
using SondaMQ.AdministradorModeloMensaje.Common;

namespace SondaMQ.AdministradorModeloMensaje.Querys
{
    public class OpcionesUsuarioQuery : QueryInterfases<NOT_OPCIONES_USUARIO>
    {
        private readonly IDateTime _dateTime;
        private readonly ISession _session;
        public OpcionesUsuarioQuery(ISession session, IDateTime dateTime)
        {
            _session = session;
            _dateTime = dateTime;
        }
        public NOT_OPCIONES_USUARIO Create(NOT_OPCIONES_USUARIO opcionesUsuario)
        {
            ITransaction transaction = _session.BeginTransaction();
            //DateTime now = DateTime.Now;
            try
            {
                opcionesUsuario.id_not_opciones_usuario = Guid.NewGuid();

                var rs = _session.Save(opcionesUsuario);

                transaction.Commit();

                return opcionesUsuario;
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
        public void Update(NOT_OPCIONES_USUARIO opcionesUsuario)
        {
            ITransaction transaction = _session.BeginTransaction();

            //PropertyInfo[] properties = typeof(NOT_CANAL).GetProperties();
            try
            {
                _session.Merge(opcionesUsuario);

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
                var opcionesUsuario = _session.Get<NOT_OPCIONES_USUARIO>(id);

                _session.Delete(opcionesUsuario);

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
        public NOT_OPCIONES_USUARIO Read(int id)
        {
            throw new NotImplementedException();
        }
        public NOT_OPCIONES_USUARIO Read(Guid id)
        {
            try
            {
                var opcionesUsuario = _session.Get<NOT_OPCIONES_USUARIO>(id);
                return opcionesUsuario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<NOT_OPCIONES_USUARIO> AllRead()
        {
            try
            {
                var opcionesUsuario = _session.QueryOver<NOT_OPCIONES_USUARIO>().List();
                return opcionesUsuario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public NOT_OPCIONES_USUARIO Read(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(string filtro, NOT_OPCIONES_USUARIO objeto)
        {
            throw new NotImplementedException();
        }

        public void Update(int filtro, NOT_OPCIONES_USUARIO objeto)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid filtro, NOT_OPCIONES_USUARIO objeto)
        {
            throw new NotImplementedException();
        }
    }
}