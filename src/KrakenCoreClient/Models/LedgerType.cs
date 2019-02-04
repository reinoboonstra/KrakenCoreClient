namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Ledger type
    /// </summary>
    public enum LedgerType
    {
        /// <summary>
        /// All (Default)
        /// </summary>
        All,

        /// <summary>
        /// Deposit
        /// </summary>
        Deposit,

        /// <summary>
        /// Withdrawal
        /// </summary>
        Withdrawal,

        /// <summary>
        /// Trade
        /// </summary>
        Trade,

        /// <summary>
        /// Margin
        /// </summary>
        Margin
    }
}