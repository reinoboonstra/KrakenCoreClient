namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Order status
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Order pending book entry
        /// </summary>
        Pending,

        /// <summary>
        /// Open order
        /// </summary>
        Open,

        /// <summary>
        /// Closed order
        /// </summary>
        Closed,

        /// <summary>
        /// Canceled order
        /// </summary>
        Canceled,

        /// <summary>
        /// Expired order
        /// </summary>
        Expired
    }
}