using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditQueue.Models
{
    public class NewReadRequest
    {
        public long Id { get; set; }
        public int ClientId { get; set; }
        public string LicenseState { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime Violationdate { get; set; }
        public string Location { get; set; }
        public string GPSCoordinates { get; set; }
        public int ThresholdSpeed { get; set; }
        public decimal TotalDistanceTravelled { get; set; }
        public decimal TotalTimeTaken { get; set; }
        public string MetricUnitSystem { get; set; }
        public string SystemId { get; set; }


        public List<DtoMedia> Medias { get; set; }
    }

    public class DtoMedia
    {
        public string ImageId { get; set; }

    }
}
