using System.Collections.Generic;

namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Order book
    /// </summary>
    public class OrderBook
    {
        /// <summary>
        /// Pair name
        /// </summary>
        public string Pair { get; set; }

        /// <summary>
        /// Ask side array of array entries(<price>, <volume>, <timestamp>)
        /// </summary>
        public IEnumerable<BookEntry> Asks { get; set; }

        /// <summary>
        /// Bid side array of array entries(<price>, <volume>, <timestamp>)
        /// </summary>
        public IEnumerable<BookEntry> Bids { get; set; }
    }
}