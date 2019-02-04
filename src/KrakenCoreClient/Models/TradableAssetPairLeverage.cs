namespace KrakenCoreClient.Models
{
    /// <inheritdoc />
    /// <summary>
    /// Tradable asset pair leverage
    /// </summary>
    public class TradableAssetPairLeverage : ITradableAssetPair
    {
        /// <inheritdoc />
        /// <summary>
        /// Pair name
        /// </summary>
        public string Pair { get; set; }

        /// <summary>
        /// Array of leverage amounts available when buying
        /// </summary>
        public int[] LeverageBuy { get; set; }

        /// <summary>
        /// Array of leverage amounts available when selling
        /// </summary>
        public int[] LeverageSell { get; set; }
    }
}