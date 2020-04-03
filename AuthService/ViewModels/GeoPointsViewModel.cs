namespace AuthService.ViewModels
{
    public class GeoPointsViewModel
    {
        public double FromLat { get; set; }
        public double FromLong { get; set; }

        public double ToLat { get; set; }
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