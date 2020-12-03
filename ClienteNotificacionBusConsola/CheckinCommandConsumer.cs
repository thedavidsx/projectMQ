using System;
using System.Threading.Tasks;
using MassTransit;
using SondaMQ.AdministradorBus;
using System.Collections.Generic;
using System.Text;

namespace ClienteNotificacionBusConsola
{
    public class CheckinCommandConsumer : IConsumer<ContratoSuscriptor>
    {

        private string _userLlega;

        public CheckinCommandConsumer(string userLlega)
        {
            _userLlega = userLlega;
        }
        public Task Consume(ConsumeContext<ContratoSuscriptor> context)
        {
 

            var command = context.Message;

            Console.WriteLine("ESTE MEMSAJE " + command.Mensaje + ", ES PARA " + _userLlega);

            //context.Publish<CheckinCompletedEvent>(new
            //{
            //    command.Id,
            //    IsOk = DateTime.Now.Millisecond % 2 == 0
            //});

            return Task.FromResult(0);
        }
    }
}
