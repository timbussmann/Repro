using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using SimpleDemo.Messages;

namespace SimpleDemo.A
{
    class Program
    {
        // When started within VisualStudi, the application crashes immediately on Endpoint.Start
        // When started via dotnet run, the application crashes when sending a message
        static async Task Main(string[] args)
        {
            // Setting the culture to en-US resolves the issue
//            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
//            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            // This is the code in NServiceBus.Core triggering the exception. 
            // Calling this code here makes the exception to go away
            // but if FirstChanceExceptions are enabled, I notice FileLoadException when trying to load System.Private.CoreLib
//            var serializer = new DataContractJsonSerializer(typeof(string));
//            var memoryStream = new MemoryStream();
//            serializer.WriteObject(memoryStream, "test");

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
                        var sendOptions = new SendOptions();
                        sendOptions.RouteToThisEndpoint();
                        await endpoint.Send(new DemoCommand(), sendOptions);
                        Console.WriteLine("message sent");
                        break;
                }
            }
        }
    }
}
