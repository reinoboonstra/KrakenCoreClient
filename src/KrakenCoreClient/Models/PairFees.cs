namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Pair fees
    /// </summary>
    public class PairFees
    {
        /// <summary>
        /// Pair name
        /// </summary>
        public string Pair { get; set; }

        /// <summary>
        /// Current fee in percent
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// Minimum fee for pair (if not fixed fee)
        /// </summary>
        public decimal MinimumFee { get; set; }

        /// <summary>
        /// Maximum fee for pair (if not fixed fee)
        /// </summary>
        public decimal MaximumFee { get; set; }

        /// <summary>
        /// Next tier's fee for pair (if not fixed fee.  nil if at lowest fee tier)
        /// </summary>
        public decimal NextFee { get; set; }

        /// <summary>
        /// Volume level of next tier (if not fixed fee.  nil if at lowest fee tier)
        /// </summary>
        public decimal NextVolume { get; set; }

        /// <summary>
        /// Volume level of current tier (if not fixed fee.  nil if at lowest fee tier)
        /// </summary>
        public decimal TierVolume { get; set; }
    }
}