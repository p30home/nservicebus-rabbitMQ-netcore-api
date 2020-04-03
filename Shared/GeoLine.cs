using NServiceBus;

namespace Shared
{
    public class GeoLine : IMessage
    {
        public GeoLine()
        {
            
        }

        public GeoLine(GeoLine geoLine)
        {
            this.FromLat = geoLine.FromLat;
            this.FromLong = geoLine.FromLong;
            this.ToLat = geoLine.ToLat;
            this.ToLong = geoLine.ToLong;
        }
        public double FromLat { get; set; }
        public double FromLong { get; set; }
        public double ToLat { get; set; }
        public double ToLong { get; set; }
    }
}