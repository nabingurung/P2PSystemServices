using ReadApi.Models;

namespace ReadApi.DTOModels
{
    public class DtoViolation
    {
      //  public long Id { get; set; }
        public int ClientId { get; set; }
        public string LicenseState { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime Violationdate { get; set; }
        public int Status { get; set; }
        public string Location { get; set; }
        public string GPSCoordinates { get; set; }
        public int ThresholdSpeed { get; set; }
        public decimal VehicleSpeed { get; set; }
        public decimal TotalDistanceTravelled { get; set; }
        public string MetricUnitSystem { get; set; }
        public string SystemId { get; set; }
        public DateTime TransactionDate { get; set; }
        public List<DtoMedia> Medias { get; set; }

    }
}
