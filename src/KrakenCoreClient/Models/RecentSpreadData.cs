using System.Collections.Generic;

namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Recent trades
    /// </summary>
    public class RecentSpreadData
    {
        /// <summary>
        /// Pair name
        /// </summary>
        public string Pair { get; set; }

        public IEnumerable<SpreadEntry> Entries { get; set; }

        public long Last { get; set; }
    }
}