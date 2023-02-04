using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadApi.Application.Services
{
    public class ViolationService : IViolationService
    {
        private readonly ILogger<ViolationService> logger;

        public ViolationService(ILogger<ViolationService> logger)
        {
            this.logger = logger;
        }
        public double CalculateSpeed(double distanceMeter, long firstSystemReadTime, long secondSystemReadTime )
        {
            var totalTimeTaken = firstSystemReadTime - secondSystemReadTime;
           var totalSeconds = DateTimeOffset.FromUnixTimeSeconds(totalTimeTaken).Second;
            
            var kph = (distanceMeter / 1000) / (totalSeconds / 3600);
            var mph = kph / 1.609;
            var mps = distanceMeter / totalSeconds;
            logger.LogInformation($"KPH : {kph}");
            logger.LogInformation($"MPH : {mph}");
            logger.LogInformation($"MPS : {mps}");
           

            return kph;
            
        }

        public DateTime GetViolationDate(long readTimeStamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(readTimeStamp).UtcDateTime;
        } 
    }
}
