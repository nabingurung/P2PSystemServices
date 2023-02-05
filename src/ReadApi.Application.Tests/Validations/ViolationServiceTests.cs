using Microsoft.Extensions.Logging;
using Moq;
using ReadApi.Application.Services;

namespace ReadApi.Application.Tests.Validations
{
    public class ViolationServiceTests
    {
        private readonly ViolationService violationService;
        private readonly Mock<ILogger<ViolationService>> mockLogger;
        public ViolationServiceTests()
        {
            mockLogger= new Mock<ILogger<ViolationService>>();
            violationService = new ViolationService(mockLogger.Object);
        }

        [Theory]
        [InlineData(2000, 1675209001, 1675209011)]
        [InlineData(1000, 1675015158296, 1675209001)]
        [InlineData(1000, 1675209001, 1675015158296)]
        [InlineData(1000, 1675015158296, 1775015158296)]
        public void CalculateSpeed_InvalidTime_ArgumentException(double distanceMeter, long firstSystemReadTime, long secondSystemReadTime)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                violationService.CalculateSpeed(distanceMeter, firstSystemReadTime, secondSystemReadTime);
            });
        }


        // used this online calculator
        // https://www.calculatorsoup.com/calculators/math/speed-distance-time-calculator.php
        [Fact]
        public void CalculateSpeed_ValidData_CorrectVehicleSpeed()
        {
           Assert.Equal(60, violationService.CalculateSpeed(1000, 1675536624, 1675536564));
          // Assert.Equal(171.429, violationService.CalculateSpeed(1000,1675536621, 1675536600));
           Assert.Equal(180, violationService.CalculateSpeed(1000,1675536620, 1675536600));
        }
    }
}
