namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Order
    /// </summary>
    public class BookEntry
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
        public long Time { get; set; }
    }
}