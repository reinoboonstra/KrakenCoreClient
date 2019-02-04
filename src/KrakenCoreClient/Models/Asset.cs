using Newtonsoft.Json;

namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Asset
    /// </summary>
    public class Asset
    {
        /// <summary>
        /// Asset name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Alternative name
        /// </summary>
        public string AltName { get; set; }

        /// <summary>
        /// Asset class
        /// </summary>
        public string AClass { get; set; }

        /// <summary>
        /// Scaling decimal places for record keeping
        /// </summary>
        public int Decimals { get; set; }

        /// <summary>
        /// Scaling decimal places for output display
        /// </summary>
        public int DisplayDecimals { get; set; }
    }
}