using System.Collections.Generic;

namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Recent trades
    /// </summary>
    public class RecentTrades
    {
        /// <summary>
        /// Pair name
        /// </summary>
        public string Pair { get; set; }

        /// <summary>
        /// List of trades
        /// </summary>
        public IEnumerable<TradeEntry> Trades { get; set; }

        /// <summary>
        /// Last
        /// </summary>
        public long Last { get; set; }
    }
}