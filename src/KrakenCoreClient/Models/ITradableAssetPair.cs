namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Tradable asset pairs interface
    /// </summary>
    public interface ITradableAssetPair
    {
        /// <summary>
        /// Pair name
        /// </summary>
        string Pair { get; set; }
    }
}