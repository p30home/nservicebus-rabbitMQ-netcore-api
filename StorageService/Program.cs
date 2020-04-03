using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using StorageService.Models;

namespace StorageService
{
    class Program
    {
        static async Task Main(string[] args)
        {


            Console.Title = "GeoAPI.StorageService";
            var endpointConfiguration = new EndpointConfiguration("GeoAPI.StorageService");
            endpointConfiguration.EnableCallbacks(makesRequests: false);
            // endpointConfiguration.UsePersistence<InMemoryPersistence>();
            // endpointConfiguration.UseTransport<LearningTransport>();
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            transport.ConnectionString("host=localhost;username=guest;password=guest");
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.RegisterComponents(
                registration: configureComponents =>
                {
                    configureComponents.ConfigureComponent<AppDbContext>(componentFactory: factory =>
                    {
                        var optionBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("in-memory");
                        return new Models.AppDbContext(optionBuilder.Options);
                    }, DependencyLifecycle.InstancePerCall);
                }
            );

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
