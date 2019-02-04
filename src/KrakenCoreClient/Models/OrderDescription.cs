namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Order description
    /// </summary>
    public class OrderDescription
    {
        /// <summary>
        /// Order description
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// Conditional close order description (if conditional close set)
        /// </summary>
        public string Close { get; set; }
    }
}