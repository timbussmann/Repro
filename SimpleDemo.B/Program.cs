using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using NServiceBus;

namespace SimpleDemo.B
{
    class Program
    {
        static void Main(string[] args)
        {

            //uncomment these lines to resolve 'Infinite recursion during resource lookup within System.Private.CoreLib' issues:
            //var serializer = new DataContractJsonSerializer(typeof(string));
            //var memoryStream = new MemoryStream();
            //serializer.WriteObject(memoryStream, "test");

            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            var endpointConfiguration = new EndpointConfiguration("SimpleDemo.B");

            endpointConfiguration.UseTransport<LearningTransport>();

            var endpoint = await Endpoint.Start(endpointConfiguration);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}