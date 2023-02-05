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
            // check if both the timestamp are valid values 
            try { DateTimeOffset.FromUnixTimeSeconds(firstSystemReadTime);  } catch { throw new ArgumentException("Invalid timestamp.");  }
            try { DateTimeOffset.FromUnixTimeSeconds(secondSystemReadTime);  } catch { throw new ArgumentException("Invalid timestamp.");  }


            var totalTimeTaken = firstSystemReadTime - secondSystemReadTime;
            if(totalTimeTaken < 0)
            {
                throw new ArgumentException("Something is wrong. We are going backwards. " +
                    "Check the read time from both the systems. Make sure to pass the read time in correct order");
            } 
          // var totalSeconds = DateTimeOffset.FromUnixTimeSeconds(totalTimeTaken).Second;

            
            float convertMeterToKM = Convert.ToSingle(distanceMeter);
            float convertSecondsToHR = Convert.ToSingle(totalTimeTaken);

            var kph = (convertMeterToKM / 1000.0f) / (convertSecondsToHR / 3600.0f);
            var mph = kph / 1.609;
            var mps = distanceMeter / totalTimeTaken;
            logger.LogInformation($"KPH : {kph}");
            logger.LogInformation($"MPH : {mph}");
            logger.LogInformation($"MPS : {mps}");
           
           kph =  (float)Math.Round( kph, 2 );

           
            return kph;
            
        }

        public DateTime GetViolationDate(long readTimeStamp)
        {
            try { DateTimeOffset.FromUnixTimeSeconds(readTimeStamp); } catch { throw new ArgumentException("Invalid timestamp"); }

            return DateTimeOffset.FromUnixTimeSeconds(readTimeStamp).UtcDateTime;
        } 

    }
}
