namespace Shared
{
    public class GeoLineResponse : GeoLineRequest
    {
        public GeoLineResponse() : base()
        {

        }


        public GeoLineResponse(GeoLineRequest geoLineRequest, double distance) : base(geoLineRequest)
        {
            this.Distance = distance;
        }
        public double Distance { get; set; }
    }
}