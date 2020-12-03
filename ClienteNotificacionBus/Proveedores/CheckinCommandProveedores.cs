using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using SondaMQ.AdministradorBusMensajeStandard;
using SondaMQ.ClienteNotificacionBus.Hubs;
using Microsoft.AspNetCore.SignalR;
using SondaMQ.AdministradorModeloMensaje.Common;
using Microsoft.Extensions.Configuration;

namespace SondaMQ.ClienteNotificacionBus.Proveedores
{
    public class CheckinCommandProveedores : IConsumer<ContratoSuscriptor>
    {
        private readonly ChatHub _hub;
        public TraceLog _log;
        public IConfiguration configuration;

        public CheckinCommandProveedores(IHubCallerClients clients, HubCallerContext context, IGroupManager group)
        {
            _log = new TraceLog();
            _hub = new ChatHub(configuration);
            try
            {
                _hub.Clients = clients;
                _hub.Context = context;
                _hub.Groups = group;

            }
            catch (Exception e)
            {

                string mensajeerror = e.Message;
                TraceLog.LogTrace("ERROR", "Cliente Notificacion", mensajeerror);

            }
        }

        public Task Consume(ConsumeContext<ContratoSuscriptor> context)
        {
            try
            {
                _log = new TraceLog();
                TraceLog.LogTrace("INFO", "Cliente Notificacion", "Menssaje Recibido por el consumidor");
                _hub.EnviarAsync(context.Message.Usuario, context.Message.Mensaje,context.Message.Asunto,context.Message.Filtro, context.Message.IdmensajeUsuario);
               
            }
            catch (Exception e) {
                TraceLog.LogTrace("ERROR", "Cliente Notificacion", e.Message);
            }

            return Task.CompletedTask;
        }
    }
}
