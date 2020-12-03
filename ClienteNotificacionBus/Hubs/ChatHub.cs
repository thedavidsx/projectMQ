using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using SondaMQ.AdministradorModeloMensaje.Common;
using SondaMQ.ClienteNotificacionBus.Fabrica;
using SondaMQ.ClienteNotificacionBus.Modelo;
using SondaMQ.AdministradorModeloMensaje.Conect;
using SondaMQ.AdministradorModeloMensaje.Model;
using SondaMQ.AdministradorModeloMensaje.Querys;

namespace SondaMQ.ClienteNotificacionBus.Hubs
{
    public class ChatHub : Hub 
    {
        static ConectFactory sm = ConectFactory.Instance;

        private static NHibernate.ISession session = sm.Session;

        MensajeUsuarioQuery usuario = new MensajeUsuarioQuery(session, null);

        public IConfiguration configuration;
        public static List<ClientesModel> ConnectedUsers { get; set; } = new List<ClientesModel>();
        public TraceLog _log;
        public MessageBusConfig msconfig;

        public static IConfiguration _configuration;
        
        public ChatHub (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [EnableCors]
        public override async Task OnConnectedAsync()
        {
            try
            {
                string user = Context.GetHttpContext().Request.QueryString.Value.Split("=")[1].Split("&")[0];

                string appBus = Context.GetHttpContext().Request.QueryString.Value.Split("=")[2].Split("&")[0];

                //Groups.a  .updateUsers(ConnectedUsers.Count(), ConnectedUsers.Select(u => u.Username));

                await Groups.AddToGroupAsync(user, "SignalR Users");


                await base.OnConnectedAsync();

                ClientesModel cliente = new ClientesModel()
                {
                    Username = user,
                    ID = Context.ConnectionId
                };
                msconfig = new MessageBusConfig(_configuration);
                MessageBusConfig.Configure(appBus, this);
                TraceLog.LogTrace("INFO", "Cliente Notificacion", "Usurio:" + user + " se a conectado");
            }
            catch (Exception e) {
                TraceLog.LogTrace("ERROR", "Cliente Notificacion", e.Message);
            }
        }

        public void EnviarAsync(string user, string message,string asunto, string filtro,Guid idmensajeUsuario)
        {
            Task.Run(async () =>
            {
                try
                {
                    NOT_MENSAJE_USUARIO appVal = usuario.Read(idmensajeUsuario);

                    appVal.fecha_entrega = DateTime.Now;
                    appVal.estado = 1;
                    usuario.Update(appVal);

                    TraceLog.LogTrace("INFO", "Cliente Notificacion", "Notificacion desplegada al usurio:" + user);
                    await Clients.All.SendAsync("enviamensaje", user, asunto, message);
                }
                catch (Exception e) {
                    TraceLog.LogTrace("ERROR", "Cliente Notificacion", e.Message);
                }
                //await Clients.User(user).SendAsync("enviamensaje", user, message);

                //await Clients.Group("SignalR Users").SendAsync("ReceiveMessage", message);
                
            });
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("enviamensaje", user, message);
        }
    }   
}