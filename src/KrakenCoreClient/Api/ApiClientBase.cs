using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KrakenCoreClient.Api.Extensions;
using KrakenCoreClient.Api.Kraken;
using KrakenCoreClient.Exceptions;
using Microsoft.Extensions.Configuration;

namespace KrakenCoreClient.Api
{
    /// <summary>
    /// API client base class
    /// </summary>
    public class ApiClientBase
    {
        #region Constants

        private const string PutVerb = "put";
        private const string PostVerb = "post";

        #endregion

        #region Private fields

        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly RateGate.RateGate rateGate;

        #endregion

        /// <summary>
        /// Base url for the API
        /// </summary>
        protected string BaseUrl;

        /// <summary>
        /// Content type for the data
        /// </summary>
        protected string ContentType;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="configuration"></param>
        public ApiClientBase(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;

            rateGate = new RateGate.RateGate(1,
                TimeSpan.FromSeconds(configuration.GetValue<int>("KrakenApi:RateGate")));
        }

        /// <summary>
        /// Create http client
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected HttpClient CreateHttpClientAsync(CancellationToken cancellationToken)
        {
            var client = httpClientFactory.CreateClient("ApiClient");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(ContentType));

            return client;
        }

        /// <summary>
        /// Get a private request message
        /// </summary>
        /// <param name="method"></param>
        /// <param name="partialUrl"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        protected HttpRequestMessage GetPrivateRequestMessage(string method, string partialUrl, object postData = null)
        {
            var nonce = GetNonce();
            postData = GetPostDataWithNonce(postData, nonce);
            var requestMessage = GetRequestMessage(method, partialUrl, postData);
            requestMessage.Headers.Add("API-Key", configuration["KrakenApi:ApiKey"]);

            requestMessage.Headers.Add("API-Sign",
                GetSignature(postData, requestMessage.RequestUri.AbsolutePath, nonce));
            
            return requestMessage;
        }

        /// <summary>
        /// Get request message
        /// </summary>
        /// <param name="method"></param>
        /// <param name="partialUrl"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        protected HttpRequestMessage GetRequestMessage(string method, string partialUrl, object postData = null)
        {
            var requestMessage = new HttpRequestMessage
            {
                Method = new HttpMethod(method),
                RequestUri = GetRequestUri(partialUrl)
            };

            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));

            if (postData == null || method.ToLower() != PostVerb && method.ToLower() != PutVerb)
            {
                return requestMessage;
            }

            requestMessage.Content = new StringContent(postData.ToString(), Encoding.UTF8, ContentType);

            return requestMessage;
        }

        /// <summary>
        /// Get response message
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async Task<HttpResponseMessage> GetResponseMessage(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var client = CreateHttpClientAsync(cancellationToken);
            rateGate.WaitToProceed();

            return await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Handle response message
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        protected async Task<KrakenResult> HandleResponseMessage(HttpResponseMessage response)
        {
            try
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return await response.ConvertToOutput();
                    case HttpStatusCode.NoContent:
                        return default;
                    default:
                        var errorContent = await response.Content.ReadAsStringAsync();

                        throw new ClientException("An unexpected error occurred while retrieving API data.")
                        {
                            HttpStatusCode = response.StatusCode,
                            ApiFaultMessage = errorContent,
                        };
                }
            }
            finally
            {
                response?.Dispose();
            }
        }

        #region Private methods

        private Uri GetRequestUri(string partialUrl)
        {
            var urlBuilder = new StringBuilder();
            urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : string.Empty).Append(partialUrl);
            var url = urlBuilder.ToString();

            return new Uri(url, UriKind.RelativeOrAbsolute);
        }

        private static long GetNonce()
        {
            return DateTime.Now.Ticks;
        }

        private static object GetPostDataWithNonce(object postData, long nonce)
        {
            return postData == null ? $"nonce={nonce}" : $"nonce={nonce}&{postData}";
        }

        private string GetSignature(object postData, string absolutePath, long nonce)
        {
            var decodedSecret = Convert.FromBase64String(configuration["KrakenApi:Secret"]);
            var hash256Bytes = (nonce + Convert.ToChar(0) + postData.ToString()).Sha256Hash();
            var pathBytes = Encoding.UTF8.GetBytes(absolutePath);
            var z = new byte[pathBytes.Length + hash256Bytes.Length];
            pathBytes.CopyTo(z, 0);
            hash256Bytes.CopyTo(z, pathBytes.Length);

            return Convert.ToBase64String(decodedSecret.GetHmacSha512Hash(z));
        }

        #endregion
    }
}