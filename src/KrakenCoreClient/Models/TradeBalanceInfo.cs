namespace KrakenCoreClient.Models
{
    /// <summary>
    /// TradeEntry balance info
    /// </summary>
    public class TradeBalanceInfo
    {
        /// <summary>
        /// Equivalent balance (combined balance of all currencies)
        /// </summary>
        public decimal EquivalentBalance { get; set; }

        /// <summary>
        /// TradeEntry balance (combined balance of all equity currencies)
        /// </summary>
        public decimal TradeBalance { get; set; }

        /// <summary>
        /// Margin amount of open positions
        /// </summary>
        public decimal MarginAmount { get; set; }

        /// <summary>
        /// Unrealized net profit/loss of open positions
        /// </summary>
        public decimal UnrealizedNetResult { get; set; }

        /// <summary>
        /// Cost basis of open positions
        /// </summary>
        public decimal CostBasis { get; set; }

        /// <summary>
        /// Current floating valuation of open positions
        /// </summary>
        public decimal CurrentFloatingValuation { get; set; }

        /// <summary>
        /// Equity = trade balance + unrealized net profit/loss
        /// </summary>
        public decimal Equity { get; set; }

        /// <summary>
        /// Free margin = equity - initial margin (maximum margin available to open new positions)
        /// </summary>
        public decimal FreeMargin { get; set; }

        /// <summary>
        /// Margin level = (equity / initial margin) * 100
        /// </summary>
        public decimal? MarginLevel { get; set; }
    }
}