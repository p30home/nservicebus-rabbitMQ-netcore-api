using System;
using Xunit;

namespace GeoCalcService.Tests
{
    public class GeoDistance_InappropriateInput
    {
        [Theory]
        [InlineData(100,50,50,50)]
        [InlineData(-2222,50,50,50)]
        [InlineData(50,50,200,50)]
        [InlineData(50,400,00,0)]
        [InlineData(50,400,50,400)]
        [InlineData(400,4000,4000,4000)]
        [InlineData(-400,-400,-400,-400)]
        public void GeoDistanceCalculator_OffboundInput(double fromLat, double fromLong, double toLat, double toLong)
        {
            Assert.Throws(typeof(Exceptions.OffboundException), () => GeoCalcService.GeoDistanceCalculator.Calculate(fromLat, fromLong, toLat, toLong));
        }
    }
}