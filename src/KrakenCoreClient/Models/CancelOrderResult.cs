namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Cancel order result
    /// </summary>
    public class CancelOrderResult
    {
        /// <summary>
        /// Number of orders canceled
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// If set, order(s) is/are pending cancellation
        /// </summary>
        public int Pending { get; set; }
    }
}