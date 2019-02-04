namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Order type
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// Market
        /// </summary>
        Market,

        /// <summary>
        /// Price = limit price
        /// </summary>
        Limit,

        /// <summary>
        /// Price = stop loss price
        /// </summary>
        StopLoss,

        /// <summary>
        /// Price = take profit price
        /// </summary>
        TakeProfit,

        /// <summary>
        /// Price = stop loss price, secondary price = take profit price
        /// </summary>
        StopLossProfit,

        /// <summary>
        /// Price = stop loss price, secondary price = take profit price
        /// </summary>
        StopLossProfitLimit,

        /// <summary>
        /// Price = stop loss trigger price, secondary price = triggered limit price
        /// </summary>
        StopLossLimit,

        /// <summary>
        /// Price = take profit trigger price, secondary price = triggered limit price
        /// </summary>
        TakeProfitLimit,

        /// <summary>
        /// Price = trailing stop offset
        /// </summary>
        TrailingStop,

        /// <summary>
        /// Price = trailing stop offset, secondary price = triggered limit offset
        /// </summary>
        TrailingStopLimit,

        /// <summary>
        /// Price = stop loss price, secondary price = limit price
        /// </summary>
        StopLossAndLimit,

        /// <summary>
        /// Settle position
        /// </summary>
        SettlePosition
    }
}