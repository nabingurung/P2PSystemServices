using System.Net.Http.Headers;

namespace AuditQueue.Services.Common
{
    public class ApiProxyResponse
    {
        public int StatusCode { get; set; }

        public object Body { get; set; }

        public HttpHeaders Headers { get; set; }
    }
}
