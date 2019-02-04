using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using KrakenCoreClient.Api.Kraken;
using KrakenCoreClient.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KrakenCoreClient.Api.Extensions
{
    /// <summary>
    /// response data extensions
    /// </summary>
    public static class ResponseExtensions
    {
        /// <summary>
        /// Convert response data to output type safe object
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static async Task<KrakenResult> ConvertToOutput(this HttpResponseMessage response)
        {
            var responseData = response.Content == null
                ? null
                : await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            KrakenResult result;

            try
            {
                var rawResult = JsonConvert.DeserializeObject<JObject>(responseData);
                result = rawResult.ToObject<KrakenResult>();
            }
            catch (Exception exception)
            {
                throw new SerializationException("Could not deserialize the response body.", exception);
            }

            if (result.Error == null || !result.Error.Any())
            {
                return result;
            }

            var message = string.Join(",", result.Error);
            var clientException = new ClientException(message);

            if (message.Contains("Permission denied"))
            {
                clientException.HttpStatusCode = HttpStatusCode.Forbidden;
            } else if (message.Contains("Invalid signature"))
            {
                clientException.HttpStatusCode = HttpStatusCode.PreconditionFailed;
            } else if (message.Contains("Invalid arguments"))
            {
                clientException.HttpStatusCode = HttpStatusCode.BadRequest;
            }

            throw clientException;
        }
    }
}