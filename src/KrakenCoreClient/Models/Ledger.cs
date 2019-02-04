namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Ledger info
    /// Note: Times given by ledger ids are more accurate than unix timestamps.
    /// </summary>
    public class Ledger
    {
        /// <summary>
        /// Ledger Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Reference id
        /// </summary>
        public string RefId { get; set; }

        /// <summary>
        /// Unix timestamp of ledger
        /// </summary>
        public decimal Time { get; set; }

        /// <summary>
        /// Type of ledger entry
        /// </summary>
        public LedgerType Type { get; set; }

        /// <summary>
        /// Asset class
        /// </summary>
        public AssetClass AssetClass { get; set; }

        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; }

        /// <summary>
        /// Transaction amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Transaction fee
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// Resulting balance
        /// </summary>
        public decimal Balance { get; set; }
    }
}