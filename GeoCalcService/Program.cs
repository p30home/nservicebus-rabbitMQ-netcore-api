using System;
using System.Threading.Tasks;
using NServiceBus;

namespace GeoCalcService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Samples.AsyncPages.Server";
            var endpointConfiguration = new EndpointConfiguration("Samples.AsyncPages.Server");
            endpointConfiguration.EnableCallbacks(makesRequests: false);
            // endpointConfiguration.UsePersistence<InMemoryPersistence>();
            // endpointConfiguration.UseTransport<LearningTransport>();
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            transport.ConnectionString("host=localhost;username=guest;password=guest");
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();


            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
