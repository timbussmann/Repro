using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using SimpleDemo.Messages;

namespace SimpleDemo.A
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            LogManager.Use<DefaultFactory>().Level(LogLevel.Debug);

            //uncomment these lines to resolve 'Infinite recursion during resource lookup within System.Private.CoreLib' issues:
            //var serializer = new DataContractJsonSerializer(typeof(string));
            //var memoryStream = new MemoryStream();
            //serializer.WriteObject(memoryStream, "test");

            var endpointConfiguration = new EndpointConfiguration("SimpleDemo.A");
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            transport.Routing().RouteToEndpoint(typeof(DemoCommand), "SimpleDemo.B");

            var endpoint = await Endpoint.Start(endpointConfiguration);

            Console.WriteLine("Press [Esc] to exit, press any other key to send a message");
            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                        return;
                    default:
                        await endpoint.Send(new DemoCommand());
                        Console.WriteLine("message sent");
                        break;
                }
            }
        }
    }
}
