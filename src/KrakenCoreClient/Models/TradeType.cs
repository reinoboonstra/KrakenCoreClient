namespace KrakenCoreClient.Models
{
    /// <summary>
    /// TradeEntry type
    /// </summary>
    public enum TradeType
    {
        /// <summary>
        /// All types (default)
        /// </summary>
        All,

        /// <summary>
        /// Any position (open or closed)
        /// </summary>
        AnyPosition,

        /// <summary>
        /// Positions that have been closed
        /// </summary>
        ClosedPosition,

        /// <summary>
        /// Any trade closing all or part of a position
        /// </summary>
        ClosingPosition,

        /// <summary>
        /// Non-positional trades
        /// </summary>
        NoPosition
    }
}