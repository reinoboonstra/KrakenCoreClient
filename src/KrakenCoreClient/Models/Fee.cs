namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Fee
    /// </summary>
    public class Fee
    {
        /// <summary>
        /// Volume
        /// </summary>
        public int Volume { get; set; }

        /// <summary>
        /// Percent fee
        /// </summary>
        public decimal Percentage { get; set; }
    }
}