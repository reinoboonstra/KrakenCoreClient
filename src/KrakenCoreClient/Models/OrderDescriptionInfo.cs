namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Order description information
    /// </summary>
    public class OrderDescriptionInfo
    {
        /// <summary>
        /// The asset pair
        /// </summary>
        public string Pair { get; set; }

        /// <summary>
        /// Type of order (buy/sell)
        /// </summary>
        public BuyOrSell Type { get; set; }

        /// <summary>
        /// Order type (See Add standard order)
        /// </summary>
        public OrderType OrderType { get; set; }

        /// <summary>
        /// Primary price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Secondary price
        /// </summary>
        public decimal Price2 { get; set; }

        /// <summary>
        /// The amount of leverage
        /// </summary>
        public string Leverage { get; set; }

        /// <summary>
        /// The order description
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// The conditional close order description (if conditional close set)
        /// </summary>
        public string Close { get; set; }
    }
}