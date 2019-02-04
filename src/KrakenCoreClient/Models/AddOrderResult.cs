using System.Collections.Generic;

namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Add order result
    /// </summary>
    public class AddOrderResult
    {
        /// <summary>
        /// Order description info
        /// Order = order description
        /// Close = conditional close order description (if conditional close set)
        /// </summary>
        public OrderDescription Description { get; set; }

        /// <summary>
        /// List of transaction ids for order (if order was added successfully)
        /// </summary>
        public IEnumerable<string> TxIds { get; set; }
    }
}