namespace AuditQueue.Models
{
    public class ViolationInfo
    {
        public string Location { get; set; }      
        public string ViolationDate { get; set; }
        public string ViolationTime { get; set; }

        public decimal Speed { get; set; }
        public double GPSLatitude { get; set; }
        public double GPSLongitude { get; set;}
    }
}
