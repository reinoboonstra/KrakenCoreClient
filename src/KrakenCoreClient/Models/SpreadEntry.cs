namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Spread entry
    /// </summary>
    public class SpreadEntry
    {
        /// <summary>
        /// Time
        /// </summary>
        public long Time { get; set; }

        /// <summary>
        /// Bid
        /// </summary>
        public decimal Bid { get; set; }

        /// <summary>
        /// Ask
        /// </summary>
        public decimal Ask { get; set; }
    }
}