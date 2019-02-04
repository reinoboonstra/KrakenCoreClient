namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Ticker
    /// </summary>
    public class TickerInformation
    {
        /// <summary>
        /// Pair name
        /// </summary>
        public string Pair { get; set; }

        /// <summary>
        /// Ask: Price, Whole lot volume, Lot volume
        /// </summary>
        public AskBid Ask { get; set; }

        /// <summary>
        /// Bid: Price, Whole lot volume, Lot volume
        /// </summary>
        public AskBid Bid { get; set; }

        /// <summary>
        /// Last trade closed: Price, Lot volume
        /// </summary>
        public LastTradeClosed LastTradeClosed { get; set; }

        /// <summary>
        /// Volume: Today, Last 24 hours
        /// </summary>
        public Recent Volume { get; set; }

        /// <summary>
        /// Volume weighted average price: Today, Last 24 hours
        /// </summary>
        public Recent VolumeWeightedAveragePrice { get; set; }

        /// <summary>
        /// Number of trades: Today, Last 24 hours
        /// </summary>
        public Recent NumberOfTrades { get; set; }

        /// <summary>
        /// Low: Today, Last 24 hours
        /// </summary>
        public Recent Low { get; set; }

        /// <summary>
        /// High: Today, Last 24 hours
        /// </summary>
        public Recent High { get; set; }

        /// <summary>
        /// Today's opening price
        /// </summary>
        public decimal OpeningPriceToday { get; set; }
    }
}