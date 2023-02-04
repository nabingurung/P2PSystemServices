using AuditQueue.Models;
using AuditQueue.Services.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AuditQueue.Services
{

    public interface IHttpClientWrapper
    {
        Task<ApiProxyResponse> HttpCall(RestServiceRequest serviceRequest);
    }
    public class HttpService : IHttpService
    {
       
       private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly ILogger<HttpService> _logger;
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpService(IHttpClientFactory httpClientFactory,
            ILogger<HttpService> logger, IConfiguration configuration, IHttpClientWrapper httpClientWrapper)
        {
           _httpClientWrapper = httpClientWrapper;
            _httpClientFactory = httpClientFactory;

            _logger = logger;
            this.configuration = configuration;
        }

        public async Task<byte[]> DownloadAlprImageAsync(AlprDataDto enrichedAlprData)
        {
            _logger.LogInformation($"Download ALPR started Image for {enrichedAlprData.PlateNumber}");
            var httpRequest = new RestServiceRequest
            {
                ServiceUrl = configuration["OpenALPRHost"],
                ServiceResource = $"{enrichedAlprData.AgentUid.Trim()}/{enrichedAlprData.BestUuid}",
                QueryParameters = new Dictionary<string, string>
                                {   {"api_key",configuration["OpenAPIKey"] },
                                        {"x" ,enrichedAlprData.VehicleCordinates.x.ToString()},
                                        {"y" ,enrichedAlprData.VehicleCordinates.y.ToString()},
                                        {"height" ,enrichedAlprData.VehicleCordinates.height.ToString()},
                                        {"width" ,enrichedAlprData.VehicleCordinates.width.ToString()}
                                },
                ServiceMethod = HttpMethod.Get,
                ServiceHeaders = new Dictionary<string, string>() { { "Content-Type", "application/json" } }
            };

            var client = _httpClientFactory.CreateClient();

            string url = await BuildUri(httpRequest);
            _logger.LogInformation($" Download ALPR Image URL For licPlate {enrichedAlprData.PlateNumber} used : {url}");

            var response1 = await client.GetAsync(url);
            if (response1.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Download ALPR Image Success {enrichedAlprData.PlateNumber} status codes  {response1.StatusCode}");
               
                byte[] imageBytes = await response1.Content.ReadAsByteArrayAsync();
                _logger.LogInformation($"Download ALPR ended Image for {enrichedAlprData.PlateNumber}");
                return imageBytes;
            }
            else
            {
                _logger.LogInformation($"Download ALPR Image Error {enrichedAlprData.PlateNumber} status codes  {response1.StatusCode}");
                throw new DownloadImageException($"Image URL : {url}; Response Content : {response1}");
            }

          
        }
    


        private async Task<string> BuildUri(RestServiceRequest serviceRequest)
        {
            Uri fullUri = null;
            if (!string.IsNullOrWhiteSpace(serviceRequest?.ServiceResource))
            {
                if (serviceRequest?.PathParameters != null && serviceRequest?.PathParameters?.Count > 0)
                {
                    serviceRequest?.PathParameters?.Select(a => serviceRequest.ServiceResource = serviceRequest?.ServiceResource.Replace(string.Concat("{" + a.Key + "}"), a.Value)).ToList();
                }

                Uri baseUrl = new Uri(serviceRequest?.ServiceUrl);
                fullUri = new Uri(baseUrl, serviceRequest?.ServiceResource);
            }
            else
            {
                if (serviceRequest?.PathParameters != null && serviceRequest?.PathParameters?.Count > 0)
                {
                    serviceRequest?.PathParameters?.Select(a => serviceRequest.ServiceUrl = serviceRequest?.ServiceUrl.Replace(string.Concat("{" + a.Key + "}"), a.Value)).ToList();
                }

                fullUri = new Uri(serviceRequest?.ServiceUrl);
            }

            var returnUri = await AddParams(serviceRequest, fullUri);

            return returnUri;
        }

        private async Task<string> AddParams(RestServiceRequest serviceRequest, Uri uri)
        {
            FormUrlEncodedContent encodedContent = null;
            UriBuilder uriBuilder = new UriBuilder(uri);

            if (serviceRequest?.QueryParameters != null && serviceRequest?.QueryParameters?.Count > 0)
            {
                encodedContent = new FormUrlEncodedContent(serviceRequest?.QueryParameters);
            }

            if (encodedContent != null)
            {
                uriBuilder.Query = await encodedContent?.ReadAsStringAsync();
            }

            return uriBuilder.ToString();
        }
    }

    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper(HttpClient httpClient = null)
        {
            _httpClient = httpClient ?? new HttpClient();
        }

        public async Task<ApiProxyResponse> HttpCall(RestServiceRequest serviceRequest)
        {
            ApiProxyResponse response = new ApiProxyResponse { StatusCode = 400, Body = "Invalid Service Request" };
            if (serviceRequest == null || serviceRequest?.ServiceUrl == null || serviceRequest.ServiceMethod == null)
                return response;
            try
            {
                HttpResponseMessage result = new HttpResponseMessage();

                string requestUri = await BuildUri(serviceRequest);

                var requestMessage = new HttpRequestMessage(serviceRequest.ServiceMethod, requestUri);

                AddHeaders(serviceRequest, requestMessage);

                BuildBody(serviceRequest, requestMessage);

                result = await _httpClient.SendAsync(requestMessage);

                var contentType = result.Content.Headers.TryGetValues("Content-Type", out IEnumerable<string> headerValues) ? headerValues.FirstOrDefault() : string.Empty;

                response.Headers = result.Headers;

                if (!string.IsNullOrWhiteSpace(contentType) && string.Equals("image/jpeg", contentType, StringComparison.OrdinalIgnoreCase))
                {
                    result.EnsureSuccessStatusCode();
                    response.Body = await result.Content.ReadAsStreamAsync();
                }
                else
                    response.Body = await result.Content.ReadAsStringAsync();

                response.StatusCode = (int)result.StatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message} \r\n Stack trace: {ex.StackTrace} \r\n \r\n request:  {JsonConvert.SerializeObject(serviceRequest)}", true);
                throw;
            }

            return response;
        }

        private void AddHeaders(RestServiceRequest serviceRequest, HttpRequestMessage request)
        {
            if (serviceRequest.ServiceHeaders != null)
            {
                foreach (var item in serviceRequest.ServiceHeaders)
                {
                    if (!string.IsNullOrEmpty(item.Value) && item.Key.ToLower() != "content-type")
                    {
                        request.Headers.Add(item.Key, item.Value);
                    }
                }
            }
        }

        private async Task<string> AddParams(RestServiceRequest serviceRequest, Uri uri)
        {
            FormUrlEncodedContent encodedContent = null;
            UriBuilder uriBuilder = new UriBuilder(uri);

            if (serviceRequest?.QueryParameters != null && serviceRequest?.QueryParameters?.Count > 0)
            {
                encodedContent = new FormUrlEncodedContent(serviceRequest?.QueryParameters);
            }

            if (encodedContent != null)
            {
                uriBuilder.Query = await encodedContent?.ReadAsStringAsync();
            }

            return uriBuilder.ToString();
        }

        private void BuildBody(RestServiceRequest serviceRequest, HttpRequestMessage requestMessage)
        {
            if (serviceRequest?.ServiceRequestBody != null)
            {
                string contentType = null;
                serviceRequest?.ServiceHeaders?.TryGetValue("Content-Type", out contentType);

                if (string.IsNullOrWhiteSpace(contentType))
                {
                    contentType = serviceRequest?.ServiceHeaders?.Where
                                                    (k => string.Equals(k.Key, "Content-Type", StringComparison.OrdinalIgnoreCase))?.Select(x => x.Value).FirstOrDefault();
                }

                if (serviceRequest?.ServiceRequestBody.GetType().FullName == "System.String")
                    requestMessage.Content = new StringContent(serviceRequest.ServiceRequestBody.ToString(), Encoding.UTF8, string.IsNullOrWhiteSpace(contentType) ? "application/json" : contentType);
                else if (contentType?.ToLower() == "application/x-www-form-urlencoded")
                {
                    requestMessage.Content = new FormUrlEncodedContent(Utility.ToKeyValue(serviceRequest?.ServiceRequestBody));
                }
                else
                {
                    requestMessage.Content = new StringContent(JsonConvert.SerializeObject(serviceRequest?.ServiceRequestBody), Encoding.UTF8, serviceRequest?.ContentType);
                }
            }
        }

        private async Task<string> BuildUri(RestServiceRequest serviceRequest)
        {
            Uri fullUri = null;
            if (!string.IsNullOrWhiteSpace(serviceRequest?.ServiceResource))
            {
                if (serviceRequest?.PathParameters != null && serviceRequest?.PathParameters?.Count > 0)
                {
                    serviceRequest?.PathParameters?.Select(a => serviceRequest.ServiceResource = serviceRequest?.ServiceResource.Replace(string.Concat("{" + a.Key + "}"), a.Value)).ToList();
                }

                Uri baseUrl = new Uri(serviceRequest?.ServiceUrl);
                fullUri = new Uri(baseUrl, serviceRequest?.ServiceResource);
            }
            else
            {
                if (serviceRequest?.PathParameters != null && serviceRequest?.PathParameters?.Count > 0)
                {
                    serviceRequest?.PathParameters?.Select(a => serviceRequest.ServiceUrl = serviceRequest?.ServiceUrl.Replace(string.Concat("{" + a.Key + "}"), a.Value)).ToList();
                }

                fullUri = new Uri(serviceRequest?.ServiceUrl);
            }

            var returnUri = await AddParams(serviceRequest, fullUri);

            return returnUri;
        }
    }


    public static class Utility
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metaToken"></param>
        /// <returns></returns>
        public static IDictionary<string, string> ToKeyValue(this object metaToken)
        {
            if (metaToken == null)
            {
                return null;
            }

            JToken token = metaToken as JToken;
            if (token == null)
            {
                return ToKeyValue(JObject.FromObject(metaToken));
            }

            if (token.HasValues)
            {
                var contentData = new Dictionary<string, string>();
                foreach (var child in token.Children().ToList())
                {
                    var childContent = child.ToKeyValue();
                    if (childContent != null)
                    {
                        contentData = contentData.Concat(childContent)
                                                                         .ToDictionary(k => k.Key, v => v.Value);
                    }
                }

                return contentData;
            }

            var jValue = token as JValue;
            if (jValue?.Value == null)
            {
                return null;
            }

            var value = jValue?.Type == JTokenType.Date ?
                                            jValue?.ToString("o", CultureInfo.InvariantCulture) :
                                            jValue?.ToString(CultureInfo.InvariantCulture);

            return new Dictionary<string, string> { { token.Path, value } };
        }
    }
    
}
