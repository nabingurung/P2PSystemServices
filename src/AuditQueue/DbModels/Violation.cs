using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuditQueue.DbModels
{
    [Table("violation")]
	public partial class Violation
	{
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("client_id")]
        public int ClientId { get;set; }

        [Column("lic_state", TypeName = "varchar(5)")]
        public string LicenseState { get; set; } = default!;

        [Column("lic_number", TypeName = "varchar(15)")]
        public string LicenseNumber { get; set; } = default!;   
        
        [Column("vio_date")]
        public DateTime Violationdate { get; set; }

        [Column("status")]
        public int Status { get; set; }

        [Column("location", TypeName = "varchar(300)")]
        [MaxLength(300)]
        public string Location { get; set; } = default!;

        [Column("gps_coordinates", TypeName = "varchar(60)")]
        public string GPSCoordinates { get; set; } = default!;

        [Column("threshold_speed", TypeName = "decimal(10,5)")]
        public int ThresholdSpeed { get; set; }

        [Column("vehicle_speed", TypeName = "decimal(10,5)")]
        public decimal VehicleSpeed { get; set; }

        [Column("travel_distance", TypeName = "decimal(10,5)")]
        public decimal TotalDistanceTravelled { get; set; }

        [Column("metric_unit", TypeName = "varchar(10)")]
        public string MetricUnitSystem { get; set; } = default!;

        [Column("system_id")]
        public string SystemId { get; set; } = default!;

        [Column("trans_date")]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Media> Medias { get; set; } = new HashSet<Media>();
	}
}

