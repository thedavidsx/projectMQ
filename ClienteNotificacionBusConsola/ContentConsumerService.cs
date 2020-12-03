using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Pipeline.Filters.Latest;
using Topshelf;
using SondaMQ.AdministradorBus;
using GreenPipes;

namespace ClienteNotificacionBusConsola
{
    public class ContentConsumerService : ServiceControl
    {
        private IBusControl _bus;

        private string _user;

        public ContentConsumerService(string user)
        {
            _user = user;
        }

        public bool Start(HostControl hostControl)
        {

            //ILatestFilter<ConsumeContext<ContratoSuscriptor>> tempFilter;



            _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint(host, _user, e => {


                    //e.Consumer<CheckinCommandConsumer>();

                    e.Consumer(() => new CheckinCommandConsumer(_user));

                    //e.Handler<ContratoSuscriptor>(context => Task.FromResult(true), x =>
                    //{
                    //    x.UseLatest(c => c.Created = filtro => tempFilter = _tempFilter);
                    //});

                });

               
            });

            _bus.Start();

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _bus.Stop();

            return true;
        }
    }
}
