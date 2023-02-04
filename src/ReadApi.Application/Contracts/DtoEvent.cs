namespace ReadApi.Application.Contracts
{

    public class LocationInfo
    {
        public string id { get; set; }
        public double gpsLat { get; set; }
        public double gpsLong { get; set; }
        public int cameraSequence { get; set; }
        public double distanceFromPrevious { get; set; }
    }

    public class PreviousLocation
    {
        public string id { get; set; }
        public double gpsLat { get; set; }
        public double gpsLong { get; set; }
        public int cameraSequence { get; set; }
        public double distanceFromPrevious { get; set; }
    }

    public class DtoEvent
    {
        public int version { get; set; }
        public string id { get; set; }
        public string companyId { get; set; }
        public string systemId { get; set; }
        public string locationId { get; set; }
        public string plate { get; set; }
        public long timestamp { get; set; }
        public long insertedId { get; set; }
        public SystemInfo systemInfo { get; set; }
        public LocationInfo locationInfo { get; set; }
        public PreviousLocation previousLocation { get; set; }
    }

    public class SystemInfo
    {
        public string name { get; set; }
        public float postedSpeed { get; set; }
        public float thresholdSpeed { get; set; }
        public int locationCount { get; set; }
    }


}
