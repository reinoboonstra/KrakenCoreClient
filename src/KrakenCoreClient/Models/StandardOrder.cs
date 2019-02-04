using System.Collections.Generic;

namespace KrakenCoreClient.Models
{
    /// <summary>
    ///  Standard order
    /// </summary>
    public class StandardOrder
    {
        /// <summary>
        /// Asset pair
        /// </summary>
        public string Pair { get; set; }

        /// <summary>
        /// Type of order (buy/sell)
        /// </summary>
        public BuyOrSell Type { get; set; }

        /// <summary>
        /// Order type:
        /// Market
        /// Limit (price = limit price)
        /// Stop-loss (price = stop loss price)
        /// Take-profit (price = take profit price)
        /// Stop-loss-profit (price = stop loss price, price2 = take profit price)
        /// Stop-loss-profit-limit (price = stop loss price, price2 = take profit price)
        /// Stop-loss-limit (price = stop loss trigger price, price2 = triggered limit price)
        /// Take-profit-limit (price = take profit trigger price, price2 = triggered limit price)
        /// Trailing-stop (price = trailing stop offset)
        /// Trailing-stop-limit (price = trailing stop offset, price2 = triggered limit offset)
        /// Stop-loss-and-limit (price = stop loss price, price2 = limit price)
        /// Settle-position
        /// </summary>
        public OrderType OrderType { get; set; }

        /// <summary>
        /// Price (optional. dependent upon ordertype)
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Secondary price (optional. dependent upon ordertype)
        /// </summary>
        public decimal? Price2 { get; set; }

        /// <summary>
        /// Order volume in lots
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// Amount of leverage desired (optional. default = none)
        /// </summary>
        public string Leverage { get; set; } = "none";

        /// <summary>
        /// List of order flags (optional):
        /// viqc = volume in quote currency(not available for leveraged orders)
        /// fcib = prefer fee in base currency
        /// fciq = prefer fee in quote currency
        /// nompp = no market price protection
        /// post = post only order(available when ordertype = limit)
        /// </summary>
        public IEnumerable<string> OrderFlags { get; set; }

        /// <summary>
        /// Scheduled start time (optional):
        /// 0 = now(default)
        /// +<n> = schedule start time<n> seconds from now
        /// <n> = unix timestamp of start time
        /// </summary>
        public long? StartTm { get; set; }

        /// <summary>
        /// Expiration time (optional):
        /// 0 = no expiration(default)
        /// +<n> = expire<n> seconds from now
        /// <n> = unix timestamp of expiration time
        /// </summary>
        public long? ExpireTm { get; set; }

        /// <summary>
        /// User reference id. 32-bit signed number. (optional)
        /// </summary>
        public string UserRef { get; set; }

        /// <summary>
        /// Validate inputs only. do not submit order (optional)
        /// </summary>
        public bool? Validate { get; set; } = false;
    }
}