using System.ComponentModel.DataAnnotations;

namespace AuthService.ViewModels
{
    public class GeoPointsViewModel
    {
        [Range(-180, +180)]
        public double FromLat { get; set; }
        [Range(-90, +90)]
        public double FromLong { get; set; }
        [Range(-180, +180)]
        public double ToLat { get; set; }
        [Range(-90, +90)]
        public double ToLong { get; set; }

        public override string ToString()
        {
            return $@"{nameof(GeoPointsViewModel)}@
            {nameof(FromLat)}:{FromLat}
            {nameof(FromLong)}:{FromLong}
            {nameof(ToLat)}:{ToLat}
            {nameof(ToLong)}:{ToLong}";
        }
    }
}