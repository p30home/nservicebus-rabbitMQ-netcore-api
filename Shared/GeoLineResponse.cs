using System.Linq;

namespace Shared
{
    public class GeoLineResult : GeoLineRequest
    {
        public GeoLineResult() : base()
        {

        }


        public GeoLineResult(GeoLineRequest geoLineRequest, double distance) : base(geoLineRequest)
        {
            this.Distance = distance;
        }
        public double Distance { get; set; }

        public override string ToString()
        {
            return $@"{nameof(GeoLineResult)}@
            {base.ToString().Split('@').LastOrDefault()}
            {nameof(Distance)}:{Distance}";
            
        }
    }
}