using System.Collections.Generic;

namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Trade volume
    /// Note: If an asset pair is on a maker/taker fee schedule, the taker side is given in "fees" and maker side in "fees_maker".
    /// For pairs not on maker/taker, they will only be given in "fees".
    /// </summary>
    public class TradeVolume
    {
        /// <summary>
        /// Volume currency
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Current discount volume
        /// </summary>
        public decimal CurrentVolume { get; set; }

        /// <summary>
        /// Array of asset pairs and fee tier info (if requested)
        /// </summary>
        public IEnumerable<PairFees> Fees { get; set; }

        /// <summary>
        /// Array of asset pairs and maker fee tier info (if requested) for any pairs on maker/taker schedule
        /// </summary>
        public IEnumerable<PairFees> FeesMaker { get; set; }
    }
}