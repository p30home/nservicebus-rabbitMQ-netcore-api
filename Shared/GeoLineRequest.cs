using System.Linq;

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

        public override string ToString()
        {
            return $@"{nameof(GeoLineRequest)}@
            {base.ToString().Split('@').LastOrDefault()}
            {nameof(UserId)}:{UserId}";
        }
    }
}