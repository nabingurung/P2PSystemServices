namespace ReadApi.Application.Contracts
{
    public class DtoViolation
    {
       
        public long PKId { get; set; }
        public int ClientId { get; set; }
        public string LicenseState { get; set; } = default!;
        public string LicenseNumber { get; set; } = default!;
        public DateTime Violationdate { get; set; }
        public string locationId { get; set; } = default!;
        public string Location { get; set; } = default!;
        public double gpsLat { get; set; }
        public double gpsLong { get; set; }
        public double PostedSpeed { get; set; }
        public double ThresholdSpeed { get; set; }
        public decimal TotalDistanceTravelled { get; set; }
        public decimal TotalTimeTaken { get; set; }
        public string MetricUnitSystem { get; set; } = "km/hr";
        public string SystemId { get; set; } = default!;
        public string ImageUID { get; set; } = default!;
       
      
        public List<DtoMedia> Medias { get; set; }

    }
}
