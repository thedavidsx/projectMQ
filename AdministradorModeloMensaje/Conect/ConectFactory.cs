using System;
using Microsoft.Extensions.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using SondaMQ.AdministradorModeloMensaje.Mapping;
using SondaMQ.AdministradorModeloMensaje.Common;

namespace SondaMQ.AdministradorModeloMensaje.Conect
{
    public class ConectFactory
    {
        public TraceLog _log;
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

        private static volatile ConectFactory instance;
        private static object syncRoot = new Object();


        private static IConfiguration _configuracion;
        private static IConfigurationBuilder configurationBuilder;
        public ConectFactory()
        {
            configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("AppSettings.json");
            _configuracion = configurationBuilder.Build();
        }
        public static ConectFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ConectFactory();
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
                        var config = _configuracion;
                        var f = Fluently.Configure();
                        if (_configuracion["Connection:Provider"] != "")
                        {
                            if (_configuracion["Connection:Provider"] == "SQLSERVER")
                            {
                                f.Database(MsSqlConfiguration.MsSql2008.ConnectionString("Data Source=" + _configuracion["Connection:ConnectionStrging:Source"] + "; Initial Catalog=" + _configuracion["Connection:ConnectionStrging:Catalog"] + ";User ID=" + _configuracion["Connection:ConnectionStrging:User"] + ";Password=" + _configuracion["Connection:ConnectionStrging:Password"] + "; " + _configuracion["Connection:ConnectionStrging:Options"] + ";").ShowSql());
                            }
                            else if (_configuracion["Connection:Provider"] == "MSORACLE")
                            {
                                f.Database(OracleManagedDataClientConfiguration.Oracle10.ConnectionString("User Id = " + _configuracion["Connection:ConnectionStrging:User"] + "; Password = " + _configuracion["Connection:ConnectionStrging:Password"] + "; Data Source =" + _configuracion["Connection:ConnectionStrging:Source"]));
                            }
                        }
                        else
                        {
                            TraceLog.LogTrace("ERROR", "ADMINISTRADOR MODELO", "No exite proveedor configurado");
                            return null;
                        }

                        f.Mappings(m => m.FluentMappings.AddFromAssemblyOf<AplicacionMap>());
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