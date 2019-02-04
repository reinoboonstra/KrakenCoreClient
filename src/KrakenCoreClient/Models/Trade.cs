using System.Collections.Generic;

namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Trade
    /// </summary>
    public class Trade
    {
        /// <summary>
        /// Transaction id
        /// </summary>
        public string TxId { get; set; }

        /// <summary>
        /// Order responsible for execution of trade
        /// </summary>
        public string OrderTxId { get; set; }

        /// <summary>
        /// Asset pair
        /// </summary>
        public string Pair { get; set; }

        /// <summary>
        /// Unix timestamp of trade
        /// </summary>
        public decimal Time { get; set; }

        /// <summary>
        /// Type of order (buy/sell)
        /// </summary>
        public BuyOrSell Type { get; set; }

        /// <summary>
        /// Order type
        /// </summary>
        public OrderType OrderType { get; set; }

        /// <summary>
        /// Average price order was executed at (quote currency)
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Total cost of order (quote currency)
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Total fee (quote currency)
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// Volume (base currency)
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// Initial margin (quote currency)
        /// </summary>
        public decimal Margin { get; set; }

        /// <summary>
        /// List of miscellaneous info
        /// Closing = trade closes all or part of a position
        /// </summary>
        public IEnumerable<string> Miscellaneous { get; set; }

        /// <summary>
        /// Position status (open/closed)
        /// </summary>
        public PositionStatus? PositionStatus { get; set; }

        /// <summary>
        /// Average price of closed portion of position (quote currency)
        /// </summary>
        public decimal? ClosedPrice { get; set; }

        /// <summary>
        /// Total cost of closed portion of position (quote currency)
        /// </summary>
        public decimal? ClosedCost { get; set; }

        /// <summary>
        /// Total fee of closed portion of position (quote currency)
        /// </summary>
        public decimal? ClosedFee { get; set; }

        /// <summary>
        /// Total fee of closed portion of position (quote currency)
        /// </summary>
        public decimal? ClosedVolume { get; set; }

        /// <summary>
        /// Total margin freed in closed portion of position (quote currency)
        /// </summary>
        public decimal? ClosedMargin { get; set; }

        /// <summary>
        /// Net profit/loss of closed portion of position (quote currency, quote currency scale)
        /// </summary>
        public decimal? Net { get; set; }

        /// <summary>
        /// List of closing trades for position (if available)
        /// </summary>
        public IEnumerable<string> Trades { get; set; }
    }
}