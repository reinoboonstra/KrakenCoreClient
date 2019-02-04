namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Tradable asset pairs info
    /// </summary>
    public enum TradableAssetPairInfo
    {
        /// <summary>
        /// All info (default)
        /// </summary>
        Info,

        /// <summary>
        /// Leverage info
        /// </summary>
        Leverage,

        /// <summary>
        /// Fees schedule
        /// </summary>
        Fees,

        /// <summary>
        /// Margin info
        /// </summary>
        Margin
    }
}