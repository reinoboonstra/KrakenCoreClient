using System.Collections.Generic;

namespace KrakenCoreClient.Models
{
    /// <inheritdoc />
    /// <summary>
    /// Tradable asset pair
    /// </summary>
    public class TradableAssetPair : ITradableAssetPair
    {
        /// <inheritdoc />
        /// <summary>
        /// Pair name
        /// </summary>
        public string Pair { get; set; }

        /// <summary>
        /// Alternate pair name
        /// </summary>
        public string AltName { get; set; }

        /// <summary>
        /// Asset class of base component
        /// </summary>
        public string AClassBase { get; set; }

        /// <summary>
        /// Asset id of base component
        /// </summary>
        public string Base { get; set; }

        /// <summary>
        /// Asset class of quote component
        /// </summary>
        public string AClassQuote { get; set; }

        /// <summary>
        /// Asset id of quote component
        /// </summary>
        public string Quote { get; set; }

        /// <summary>
        /// Volume lot size
        /// </summary>
        public string Lot { get; set; }

        /// <summary>
        /// Scaling decimal places for pair
        /// </summary>
        public int PairDecimals { get; set; }

        /// <summary>
        /// Scaling decimal places for volume
        /// </summary>
        public int LotDecimals { get; set; }

        /// <summary>
        /// Amount to multiply lot volume by to get currency volume
        /// </summary>
        public int LotMultiplier { get; set; }

        /// <summary>
        /// Array of leverage amounts available when buying
        /// </summary>
        public int[] LeverageBuy { get; set; }

        /// <summary>
        /// Array of leverage amounts available when selling
        /// </summary>
        public int[] LeverageSell { get; set; }

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