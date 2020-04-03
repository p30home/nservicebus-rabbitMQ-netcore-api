using System;
using Shared;

namespace GeoCalcService
{
    public class GeoDistanceCalculator
    {
        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        /// <summary>
        /// calculates distance between two points and returns result as Killometers
        /// </summary>
        /// <param name="geoLine"></param>
        /// <returns></returns>
        public static double Calculate(GeoLine geoLine)
        {
            return Calculate(geoLine.FromLat, geoLine.FromLong, geoLine.ToLat, geoLine.ToLong);
        }

        /// <summary>
        /// calculates distance between two points and returns result as Killometers
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns></returns>
        public static double Calculate(double lat1, double lon1, double lat2, double lon2)
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;

                dist = dist * 1.609344;

                return (dist);
            }
        }
    }
}