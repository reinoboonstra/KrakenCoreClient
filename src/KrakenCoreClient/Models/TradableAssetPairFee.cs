using System.Collections.Generic;

namespace KrakenCoreClient.Models
{
    /// <inheritdoc />
    /// <summary>
    /// Tradable asset pair fee
    /// </summary>
    public class TradableAssetPairFee : ITradableAssetPair
    {
        /// <inheritdoc />
        /// <summary>
        /// Pair name
        /// </summary>
        public string Pair { get; set; }

        /// <summary>
        /// Fee schedule array in [volume, percent fee] tuples
        /// </summary>
        public IEnumerable<Fee> Fees { get; set; }

        /// <summary>
        /// Maker fee schedule array in [volume, percent fee] tuples (if on maker/taker)
        /// </summary>
        public IEnumerable<Fee> FeesMaker { get; set; }

        /// <summary>
        /// Volume discount currency
        /// </summary>
        public string FeeVolumeCurrency { get; set; }
    }
}