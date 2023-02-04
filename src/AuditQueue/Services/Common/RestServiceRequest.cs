namespace AuditQueue.Services.Common
{
    public class RestServiceRequest
    {
        public string TrackingId { get; set; }

        public string ServiceName { get; set; }
        public string ServiceUrl { get; set; }

        public string ServiceResource { get; set; }

        public Dictionary<string, string> ServiceHeaders { get; set; }

        public bool IsOAuth { get; set; }

        public string ConsumerKey { get; set; }

        public string ConsumerSecret { get; set; }

        public HttpMethod ServiceMethod { get; set; }

        public object ServiceRequestBody { get; set; }

        public int Retry { get; set; }

        public TimeSpan Delay { get; set; }

        public int Timeout { get; set; }
        public Dictionary<string, string> QueryParameters { get; set; }
        public Dictionary<string, string> PathParameters { get; set; }
        public string ContentType { get; set; }
    }
}
