using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
//using SondaMQ.AdministradorBus;
using SondaMQ.AdministradorBusMensajeStandard;


namespace SondaMQ.ProveedorNotificacionBus.Suscriptores
{
    public class CheckEnventoSuscriptores : IConsumer<ContratoSuscriptor>
    {
        public Task Consume(ConsumeContext<ContratoSuscriptor> context)
        {
            Fabrica.MessageBusConfig.CheckinResults.Add(new Modelo.CheckResultadosModel
            {
                 ContentId = context.Message.Mensaje,
                 Result = context.Message.IsOk ?  "OK" : "FALLO"
            });
            return Task.FromResult(0);
        }
    }
}
