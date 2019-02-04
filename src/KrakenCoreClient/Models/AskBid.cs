namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Ask bid
    /// </summary>
    public class AskBid
    {
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Whole lot volume
        /// </summary>
        public decimal WholeLotVolume { get; set; }

        /// <summary>
        /// Lot volume
        /// </summary>
        public decimal LotVolume { get; set; }
    }
}