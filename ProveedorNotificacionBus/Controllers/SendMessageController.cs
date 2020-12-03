using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SondaMQ.ProveedorNotificacionBus.Modelo;
//using SondaMQ.AdministradorBus;
using SondaMQ.AdministradorEmail.Services;
using SondaMQ.AdministradorModeloMensaje.Conect;
using SondaMQ.AdministradorModeloMensaje.Model;
using SondaMQ.AdministradorModeloMensaje.Querys;
using SondaMQ.AdministradorModeloMensaje.Common;
using SondaMQ.AdministradorBusMensajeStandard;
using SondaMQ.AdministradorModeloMensaje;

namespace SondaMQ.ProveedorNotificacionBus.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class SendMessageController : ControllerBase
    {

        static ConectFactory sm = ConectFactory.Instance;

        private static NHibernate.ISession session = sm.Session;

        AplicacionQuery aplicacion = new AplicacionQuery(session, null);

        MensajeQuery mensaje = new MensajeQuery(session, null);

        MensajeUsuarioQuery usuario = new MensajeUsuarioQuery(session, null);

        public EmailService _email;

        public TraceLog _log;

        //public ValidatorCommon _valEmail;

        public string appLog = "Proveedor de Mensajes";

        public static IConfiguration _configuration;

        public IConfiguration configuration;

        public SendMessageController(IConfiguration configuration)
        {
                _configuration = configuration;
        }

        // POST: api/SendMessage
        [HttpPost]
        public async Task<HttpResponseMessage> PostAsync([FromBody] object mensajeProveedorModel)
        {
            string messageResponse = "";
            _log = new TraceLog();
            _email = new EmailService(_configuration);
            String[] _canal = null;
            if (mensajeProveedorModel != null)
            {
                try
                {
                    MensajeProveedorModel mensajeProveedor = JsonConvert.DeserializeObject<MensajeProveedorModel>(mensajeProveedorModel.ToString());
                    try
                    {
                        //SE VALIDA QUE LA APLICACION ESTE REGISTRADA EN EL SONDAMQ
                        var appVal = aplicacion.Read(mensajeProveedor.cod_aplicacion);
                        if (((List<NOT_APLICACION>)appVal).Count > 0)
                        {

                            TraceLog.LogTrace("INFO", appLog, "APLICACION REGISTRADA");
                            if (mensajeProveedor.listausuarios.Count > 0)
                            {
                                NOT_MENSAJE mdMensaje = new NOT_MENSAJE
                                {
                                    id_aplicacion = ((List<NOT_APLICACION>)appVal)[0].id_not_aplicacion,
                                    prioridad = mensajeProveedor.prioridad,
                                    formato = mensajeProveedor.formato,
                                    mensaje = mensajeProveedor.mensaje,
                                    fecha_envio = DateTime.Now,
                                    asunto = mensajeProveedor.asunto
                                    
                                };
                                try
                                {
                                    mdMensaje = mensaje.Create(mdMensaje);

                                    //SE RECORRE LOS USUARIOS A LOS CUALES SE LES VA A ENVIAR LOS MENSAJES
                                    for (int i = 0; i < mensajeProveedor.listausuarios.Count; i++)
                                    {
                                        _canal = null;
                                        if (mensajeProveedor.listausuarios[i].canal != "")
                                        {
                                            _canal = mensajeProveedor.listausuarios[i].canal.Split(",");
                                            NOT_MENSAJE_USUARIO mdUsuario = new NOT_MENSAJE_USUARIO
                                            {
                                                id_usuario = mensajeProveedor.listausuarios[i].id,
                                                id_not_mensaje = mdMensaje.id_not_mensaje,
                                            };

                                            try
                                            {
                                                mdUsuario = usuario.Create(mdUsuario);
                                                try
                                                {
                                                    if (_canal[0] == "PUSH")
                                                    {
                                                        var endPointMessage = await Fabrica.MessageBusConfig.BusControl.GetSendEndpoint(new Uri(
                                                    _configuration["BusMensage:host"] + mensajeProveedor.cod_aplicacion + _configuration["BusMensage:optionHost"] + mensajeProveedor.cod_aplicacion));

                                                        await endPointMessage.Send<ContratoSuscriptor>(new
                                                        {
                                                            Mensaje = mensajeProveedor.mensaje,
                                                            Usuario = mensajeProveedor.listausuarios[i].id,
                                                            Asunto = mensajeProveedor.asunto,
                                                            IsOk = true,
                                                            IdmensajeUsuario = mdUsuario.id_not_mensaje_usuario
                                                        });
                                                    }

                                                    if (_canal[1] == "EMAIL")
                                                    {
                                                        if (ValidatorCommon.ComprobarFormatoEmail(mensajeProveedor.listausuarios[i].email))
                                                        {
                                                            await _email.SendEmailAsync(mensajeProveedor.listausuarios[i].email, mensajeProveedor.mensaje,mensajeProveedor.asunto);
                                                        }
                                                        else
                                                        {
                                                            TraceLog.LogTrace("INFO", appLog, "EL EMAIL:" + mensajeProveedor.listausuarios[i].email + " NO ES VALIDO");
                                                        }
                                                    }
                                                }
                                                catch (Exception e)
                                                {
                                                    messageResponse = e.Message.ToString();
                                                    TraceLog.LogTrace("ERROR", appLog, messageResponse);
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                messageResponse = e.Message.ToString();
                                                TraceLog.LogTrace("ERROR", appLog, messageResponse);
                                            }
                                        }
                                        else
                                        {
                                            TraceLog.LogTrace("ERROR", appLog, "CANALES NO VALIDOS");
                                        }
                                    }
                                    TraceLog.LogTrace("INFO", appLog, "MENSAJES ENCOLADOS EN EL BUS:" + _configuration["BusMensage:host"] + " EN LA COLA:" + mensajeProveedor.cod_aplicacion);
                                }
                                catch (Exception exm)
                                {
                                    messageResponse = exm.Message.ToString();
                                    TraceLog.LogTrace("ERROR", appLog, messageResponse);
                                }
                            }
                            else
                            {
                                messageResponse = "NO HAY USUARIOS A LOS CUALES ENVIAR EL MENSAJE";
                                TraceLog.LogTrace("INFO", appLog, messageResponse);
                            }

                        }
                        else{
                            messageResponse = "APLICACION NO SE ENCUENTRA REGISTRADA";
                            TraceLog.LogTrace("INFO", appLog, messageResponse);
                        }
                    }catch (Exception ex){
                        messageResponse = ex.Message.ToString();
                        TraceLog.LogTrace("ERROR", appLog, messageResponse);
                    }
                }catch (Exception e){
                    messageResponse = e.Message.ToString();
                    TraceLog.LogTrace("ERROR", appLog, messageResponse);
                }
                finally {

                }
            }
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK , ReasonPhrase = messageResponse };
        }
    }
}
