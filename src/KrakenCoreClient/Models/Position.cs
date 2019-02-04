using System.Collections.Generic;

namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Open position info
    /// Note: Unless otherwise stated, costs, fees, prices, and volumes are in the asset pair's scale, not the currency's scale.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Position transaction id
        /// </summary>
        public string TxId { get; set; }

        /// <summary>
        /// Order responsible for execution of trade
        /// </summary>
        public string OrderTxId { get; set; }

        /// <summary>
        /// Asset pair
        /// </summary>
        public string Pair { get; set; }

        /// <summary>
        /// Unix timestamp of trade
        /// </summary>
        public decimal Time { get; set; }

        /// <summary>
        /// Type of order used to open position (buy/sell)
        /// </summary>
        public BuyOrSell Type { get; set; }

        /// <summary>
        /// Order type used to open position
        /// </summary>
        public OrderType OrderType { get; set; }

        /// <summary>
        /// Opening cost of position (quote currency unless viqc set in oflags)
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Opening fee of position (quote currency)
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// Position volume (base currency unless viqc set in oflags)
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// Position volume closed (base currency unless viqc set in oflags)
        /// </summary>
        public decimal VolumeClosed { get; set; }

        /// <summary>
        /// Initial margin (quote currency)
        /// </summary>
        public decimal Margin { get; set; }

        /// <summary>
        /// Current value of remaining position (if docalcs requested.  quote currency)
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Unrealized profit/loss of remaining position (if docalcs requested.  quote currency, quote currency scale)
        /// </summary>
        public decimal Net { get; set; }

        /// <summary>
        /// List of miscellaneous info
        /// </summary>
        public IEnumerable<string> Miscellaneous { get; set; }

        /// <summary>
        /// List of order flags
        /// Viqc = volume in quote currency
        /// </summary>
        public IEnumerable<string> OrderFlags { get; set; }
    }
}