using System;
using Microsoft.Extensions.Configuration;
using MassTransit;
using SondaMQ.ClienteNotificacionBus.Hubs;
using SondaMQ.ClienteNotificacionBus.Proveedores;
using SondaMQ.AdministradorModeloMensaje.Common;

namespace SondaMQ.ClienteNotificacionBus.Fabrica
{
    public class MessageBusConfig
    {
        public static IBusControl BusControl { get; set; }

        public static IConfiguration configuration;

        private static IConfiguration _configuration;

        public TraceLog _log;

        public MessageBusConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static void Configure(String nameQueue, ChatHub hub)
        {
     
            Microsoft.AspNetCore.SignalR.IHubCallerClients clients = hub.Clients;
            Microsoft.AspNetCore.SignalR.HubCallerContext context = hub.Context;
            Microsoft.AspNetCore.SignalR.IGroupManager group = hub.Groups;

            BusControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.AutoDelete = true;
                var host = cfg.Host(new Uri(_configuration["BusMensage:host"]), h =>
                {
                    h.Username(_configuration["BusMensage:user"]);
                    h.Password(_configuration["BusMensage:password"]);
                });

                cfg.ReceiveEndpoint(host, nameQueue, e => {
                    e.Consumer(() => new CheckinCommandProveedores(clients, context, group));
                });
            });

            BusControl.Start();
        }
    }
}
