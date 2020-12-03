using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using SondaMQ.AdministradorModeloMensaje.Mapping;

namespace SondaMQ.AdministradorModeloMensaje.Conect
{
    public class SessionSqlserver
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

        private static volatile SessionSqlserver instance;
        private static object syncRoot = new Object();

        public SessionSqlserver()
        {

        }
        public static SessionSqlserver Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new SessionSqlserver();
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
                        f.Database(MsSqlConfiguration.MsSql2008.ConnectionString("Data Source=W10DRODRIGUENTB;Initial Catalog=USU_B;Persist Security Info=True;User ID=sa;Password=Sonda.2022").ShowSql());
                        f.Mappings(m => m.FluentMappings.AddFromAssemblyOf<CanalMap>());
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