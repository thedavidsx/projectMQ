using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MassTransit;
using SondaMQ.ProveedorNotificacionBus.Modelo;

namespace SondaMQ.ProveedorNotificacionBus.Fabrica
{
    public class MessageBusConfig
    {
        public static IBusControl BusControl { get; set; }

        public static IList<CheckResultadosModel> CheckinResults { get; set; }

        public static IConfiguration _configuration;
        public MessageBusConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static void Configure()
        {
            CheckinResults = new List<CheckResultadosModel>();

            BusControl = Bus.Factory. CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(_configuration["BusMensage:host"]), h =>
                {
                    h.Username(_configuration["BusMensage:user"]);
                    h.Password(_configuration["BusMensage:password"]);
                });

                #region ...

                cfg.ReceiveEndpoint(host, e => { /*e.Consumer<CheckEnventoSuscriptores>();*/ });

                //cfg.ConfigureSend(x => x.UseSendExecute(context =>
                //{
                //    context.Headers.Set("prueba", "prueba_q");
                //}));
                
                #endregion
            });

            BusControl.Start();
        }
    }
}
