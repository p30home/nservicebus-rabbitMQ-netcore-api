using System;
using Xunit;

namespace GeoCalcService.Tests
{
    public class GeoDistance_InappropriateInput
    {
        [Theory]
        [InlineData(100, 50, 50, 50)]
        [InlineData(-2222, 50, 50, 50)]
        [InlineData(50, 50, 200, 50)]
        [InlineData(50, 400, 00, 0)]
        [InlineData(50, 400, 50, 400)]
        [InlineData(400, 4000, 4000, 4000)]
        [InlineData(-400, -400, -400, -400)]
        public void GeoDistanceCalculator_OffboundInput(double fromLat, double fromLong, double toLat, double toLong)
        {
            Assert.Throws(typeof(Exceptions.OffboundException), () => GeoCalcService.GeoDistanceCalculator.Calculate(fromLat, fromLong, toLat, toLong));
        }

        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(50, 50, 50, 50)]
        [InlineData(25, 25, 25, 25)]
        [InlineData(25, 10, 25, 10)]
        [InlineData(-10, -5, -10, -5)]
        public void GeoDistanceCalculator_ZeroDistance(double fromLat, double fromLong, double toLat, double toLong)
        {
            Assert.Equal<double>(0, GeoCalcService.GeoDistanceCalculator.Calculate(fromLat, fromLong, toLat, toLong));
        }

        [Theory]
        [InlineData(30,25,75,42,5091)]
        [InlineData(-20,30,44,14,7300)]
        public void GeoDistanceCalculator_PrecalculatedDistance(double fromLat, double fromLong, double toLat, double toLong,int excpectedDistance)
        {
            Assert.Equal<int>(excpectedDistance, (int)GeoCalcService.GeoDistanceCalculator.Calculate(fromLat, fromLong, toLat, toLong));
        }
    }
}