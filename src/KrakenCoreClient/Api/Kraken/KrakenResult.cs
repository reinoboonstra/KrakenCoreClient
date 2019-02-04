using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace KrakenCoreClient.Api.Kraken
{
    /// <summary>
    /// kraken response result object
    /// </summary>
    public class KrakenResult
    {
        /// <summary>
        /// Eventual list of errors
        /// </summary>
        public IEnumerable<string> Error { get; set; }

        /// <summary>
        /// The result
        /// </summary>
        public JObject Result { get; set; }
    }
}