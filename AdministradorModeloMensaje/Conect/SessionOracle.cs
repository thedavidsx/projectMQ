using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using SondaMQ.AdministradorModeloMensaje.Mapping;
namespace SondaMQ.AdministradorModeloMensaje.Conect
{
    public class SessionOracle
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

        private static volatile SessionOracle instance;
        private static object syncRoot = new Object();
        public SessionOracle()
        {

        }

        public static SessionOracle Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new SessionOracle();
                    }
                }
                return instance;
            }
        }

        private ISession OpenSession()
        {
            return this.SessionFactory.OpenSession();
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
                        //f.Database(OracleManagedDataClientConfiguration.Oracle10.ConnectionString("Data Source=DESARROLLO; User Id=ATO01_USU; Password=ato01_usu;").DefaultSchema("ATO01_USU"));

                        f.Database(OracleManagedDataClientConfiguration.Oracle10.ConnectionString("User Id = ATO01_USU; Password = ato01_usu; Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.165.202.30)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = Desarrollo) ) )"));
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
