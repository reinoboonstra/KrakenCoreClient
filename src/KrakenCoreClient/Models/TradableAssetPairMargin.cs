namespace KrakenCoreClient.Models
{
    /// <inheritdoc />
    /// <summary>
    /// Tradable asset pair margin
    /// </summary>
    public class TradableAssetPairMargin : ITradableAssetPair
    {
        /// <inheritdoc />
        /// <summary>
        /// Pair name
        /// </summary>
        public string Pair { get; set; }

        /// <summary>
        /// Margin call level
        /// </summary>
        public decimal MarginCall { get; set; }

        /// <summary>
        /// Stop-out/liquidation margin level
        /// </summary>
        public decimal MarginStop { get; set; }
    }
}