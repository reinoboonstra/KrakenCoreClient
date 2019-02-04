namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Last trade closed
    /// </summary>
    public class LastTradeClosed
    {
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Lot volume
        /// </summary>
        public decimal LotVolume { get; set; }
    }
}