namespace KrakenCoreClient.Models
{
    /// <summary>
    /// 24 hour interface
    /// </summary>
    public class Recent
    {
        /// <summary>
        /// Today
        /// </summary>
        public decimal Today { get; set; }

        /// <summary>
        /// Last 24 hours
        /// </summary>
        public decimal Last24Hours { get; set; }
    }
}