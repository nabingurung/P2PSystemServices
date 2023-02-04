using ReadApi.Core.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadApi.Core.Entities
{
    public class Violation 
    {
        public long VioId { get; set; }
        public int ClientId { get; set; }
        public string LicenseState { get; set; } = default!;
        public string LicenseNumber { get; set; } = default!;
        public DateTime Violationdate { get; set; }
        public int VioStatus { get; set; }
        public string locationId { get; set; }
        public string Location { get; set; } = default!;
        public double gpsLat { get; set; }
        public double gpsLong { get; set; }
        public double PostedSpeed { get; set; }
        
        public double ThresholdSpeed { get; set; }
        public double VehicleSpeed { get; set; }
        public double TotalDistanceTravelled { get; set; }
        public string MetricUnitSystem { get; set; } = default!;
        public string SystemId { get; set; } = default!;
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        public string ImageUID { get; set; } = default!;

        public double TimeTaken { get; set; }
        public virtual ICollection<Media> Medias { get; set; }

        //public Violation()
        //{
        //    Medias = new HashSet<Media>();
        //}
    }
}
