using System;
using Xunit;

namespace GeoCalcService.Tests
{
    public class GeoDistance_InappropriateInput
    {
        [Theory]
        [InlineData(100,50,50,50)]
        public void GeoDistanceCalculator_OffboundInput(double fromLat, double fromLong, double toLat, double toLong)
        {
            Assert.Throws(typeof(Exceptions.OffboundException), () => GeoCalcService.GeoDistanceCalculator.Calculate(fromLat, fromLong, toLat, toLong));
        }
    }
}