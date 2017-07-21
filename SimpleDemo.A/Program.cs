using System;
using System.Threading.Tasks;
using NServiceBus;
using SimpleDemo.Messages;

namespace SimpleDemo.A
{
    class Program
    {
        // When started within VisualStudio, the application crashes immediately on Endpoint.Start
        // When started via dotnet run, the application crashes when sending a message
        static async Task Main(string[] args)
        {
            // Setting the culture to en-US resolves the issue
            // Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            // Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            // uncommenting this code resolves the issue as well:
            // var serializer = new DataContractJsonSerializer(typeof(string));
            // var memoryStream = new MemoryStream();
            // serializer.WriteObject(memoryStream, "test");

            var endpointConfiguration = new EndpointConfiguration("SimpleDemo.A");
            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            var endpoint = await Endpoint.Start(endpointConfiguration);

            await endpoint.SendLocal(new DemoCommand());

            Console.WriteLine("Message sent. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
