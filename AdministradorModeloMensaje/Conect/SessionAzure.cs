using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using SondaMQ.AdministradorModeloMensaje.Mapping;

namespace SondaMQ.AdministradorModeloMensaje.Conect
{
    public class SessionAzure
    {
        private ISessionFactory _sessionFactory;
        private ISession _session;

        public ISession Session
        {
            get
            {
                if (null == this._session || !this._session.IsOpen)
                    this._session = this.OpenSession();
                return _session;
            }
            set
            {
                this._session = value;
            }
        }

        private static volatile SessionAzure instance;
        private static object syncRoot = new Object();

        public SessionAzure()
        {

        }
        public static SessionAzure Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new SessionAzure();
                    }
                }
                return instance;
            }
        }

        private ISession OpenSession()
        {
            return this.SessionFactory.OpenSession();
        }

        public bool TestConnection()
        {
            ISessionFactory _iSession = this.SessionFactory;
            if (null == _iSession)
            {
                return false;
            }
            return true;
        }

        public ISessionFactory SessionFactory
        {
            get
            {
                try
                {
                    if (_sessionFactory == null)
                    {
                        var f = Fluently.Configure();
                        f.Database(MsSqlConfiguration.MsSql7.ConnectionString("Data Source=W10DRODRIGUENTB;Initial Catalog=USU;Persist Security Info=True;User ID=sa;Password=Sonda.2019").ShowSql());
                        //f.Mappings(m => m.FluentMappings.AddFromAssemblyOf<AplicacionMap>());
                        f.Mappings(m => m.FluentMappings.AddFromAssemblyOf<CanalMap>());
                        //f.Mappings(m => m.FluentMappings.AddFromAssemblyOf<CategoriaMap>());
                        //f.Mappings(m => m.FluentMappings.AddFromAssemblyOf<MensajeMap>());
                        //f.Mappings(m => m.FluentMappings.AddFromAssemblyOf<MensajeUsuarioMap>());
                        //f.Mappings(m => m.FluentMappings.AddFromAssemblyOf<OpcionesUsuarioMap>());
                        //f.Mappings(m => m.FluentMappings.AddFromAssemblyOf<PlantillaMap>());
                        _sessionFactory = f.BuildSessionFactory();
                    }
                    return _sessionFactory;
                }
                catch (Exception e)
                {
                    Console.WriteLine("El error es : " + e.Message);
                    return null;
                }
            }
        }
    }
}