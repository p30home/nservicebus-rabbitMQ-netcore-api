using System.Collections.Generic;
using NServiceBus;

namespace Shared
{
    public class GeoLineHistories : IMessage
    {

        public ICollection<GeoLineResult> GeoLines { get; set; }
    }
}