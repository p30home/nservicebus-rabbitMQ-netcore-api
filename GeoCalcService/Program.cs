﻿using System;
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
            endpointConfiguration.UsePersistence<LearningPersistence>();
            endpointConfiguration.UseTransport<LearningTransport>();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
