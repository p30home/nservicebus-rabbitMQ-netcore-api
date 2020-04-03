namespace Shared
{
    public class GeoLineRequest : GeoLine
    {
        public GeoLineRequest() : base()
        {
        }

        public GeoLineRequest(GeoLineRequest geoLineRequest): base(geoLineRequest)
        {
            this.UserId = geoLineRequest.UserId;
        }

        public GeoLineRequest(GeoLine geoLine,string userId) : base(geoLine)
        {
            this.UserId = userId;
        }
        public string UserId { get; set; }
    }
}