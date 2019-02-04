namespace KrakenCoreClient.Models
{
    /// <summary>
    /// TradeEntry
    /// </summary>
    public class TradeEntry
    {
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Volume
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// Time
        /// </summary>
        public float Time { get; set; }

        /// <summary>
        /// Buy/Sell
        /// </summary>
        public BuyOrSell BuySell { get; set; }

        /// <summary>
        /// Market/Limit
        /// </summary>
        public MarketOrLimit MarketLimit { get; set; }

        /// <summary>
        /// Miscellaneous
        /// </summary>
        public string Miscellaneous { get; set; }
    }
}