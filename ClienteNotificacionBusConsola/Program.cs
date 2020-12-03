using System;
using Topshelf;

namespace ClienteNotificacionBusConsola
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string user = "OtraPrueba";

            HostFactory.Run(x => {
                x.Service<ContentConsumerService>(s =>
                {
                    s.ConstructUsing(name => new ContentConsumerService(user));
                    s.WhenStarted((tc, hostControl) => tc.Start(hostControl));
                    s.WhenStopped((tc, hostControl) => tc.Stop(hostControl));

                });
                x.RunAsLocalSystem();
                x.AddCommandLineDefinition("user", v => user = v);
            });
        }
    }
}
