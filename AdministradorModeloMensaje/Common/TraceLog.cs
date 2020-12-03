using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using log4net;

namespace SondaMQ.AdministradorModeloMensaje.Common
{
    public class TraceLog
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TraceLog));
        public static XmlDocument _log4netConfig = new XmlDocument();
        //private string _path;

        public TraceLog() {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("TraceLog.config"));
            var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }

        public static void LogTrace(string type,string app,string message) {
            switch (type)
            {
                case "INFO":
                    log.Info(app + ": " + message);
                    break;
                case "DEBUG":
                    log.Debug(app + ": " + message);
                    break;
                case "ERROR":
                    log.Error(app + ": " + message);
                    break;
                default:
                    log.Fatal(app + ": " + message);
                    break;
            }
        }
    }
}
