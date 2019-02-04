namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Ohlc entry
    /// </summary>
    public class OhlcEntry
    {
        /// <summary>
        /// Time
        /// </summary>
        public long Time { get; set; }

        /// <summary>
        /// Open
        /// </summary>
        public decimal Open { get; set; }

        /// <summary>
        /// High
        /// </summary>
        public decimal High { get; set; }

        /// <summary>
        /// Low
        /// </summary>
        public decimal Low { get; set; }

        /// <summary>
        /// Close
        /// </summary>
        public decimal Close { get; set; }

        /// <summary>
        /// Volume swap
        /// </summary>
        public decimal VSwap { get; set; }

        /// <summary>
        /// Volume
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// Count
        /// </summary>
        public long Count { get; set; }
    }
}