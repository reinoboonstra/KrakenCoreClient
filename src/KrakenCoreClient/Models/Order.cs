using System.Collections.Generic;

namespace KrakenCoreClient.Models
{
    /// <summary>
    /// Open order
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Transaction id
        /// </summary>
        public string TxId { get; set; }
        /// <summary>
        /// Referral order transaction id that created this order
        /// </summary>
        public string RefId { get; set; }

        /// <summary>
        /// User reference id
        /// </summary>
        public string UserRef { get; set; }

        /// <summary>
        /// The status of order:
        /// Pending = order pending book entry
        /// Open = open order
        /// Closed = closed order
        /// Canceled = order canceled
        /// Expired = order expired
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Additional info on status (if any)
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Unix timestamp of when order was placed
        /// </summary>
        public decimal OpenTm { get; set; }

        /// <summary>
        /// Unix timestamp of when order was closed
        /// </summary>
        public decimal CloseTm { get; set; }

        /// <summary>
        /// Unix timestamp of order start time (or 0 if not set)
        /// </summary>
        public decimal? StartTm { get; set; }

        /// <summary>
        /// Unix timestamp of order end time (or 0 if not set)
        /// </summary>
        public decimal? ExpireTm { get; set; }

        /// <summary>
        /// The order description information
        /// </summary>
        public OrderDescriptionInfo Description { get; set; }

        /// <summary>
        /// Volume of order (base currency unless viqc set in order flags)
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// Volume executed (base currency unless viqc set in order flags)
        /// </summary>
        public decimal VolumeExecuted { get; set; }

        /// <summary>
        /// The total cost (quote currency unless unless viqc set in order flags)
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// The total fee (quote currency)
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// The average price (quote currency unless viqc set in order flags)
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The stop price (quote currency, for trailing stops)
        /// </summary>
        public decimal StopPrice { get; set; }

        /// <summary>
        /// The triggered limit price (quote currency, when limit based order type triggered)
        /// </summary>
        public decimal LimitPrice { get; set; }

        /// <summary>
        /// A list of miscellaneous info
        /// Stopped = triggered by stop price
        /// Touched = triggered by touch price
        /// Liquidated = liquidation
        /// Partial = partial fill
        /// </summary>
        public IEnumerable<string> Miscellaneous { get; set; }

        /// <summary>
        /// A list of order flags
        /// Viqc = volume in quote currency
        /// Fcib = prefer fee in base currency(default if selling)
        /// Fciq = prefer fee in quote currency(default if buying)
        /// Nompp = no market price protection
        /// </summary>
        public IEnumerable<string> OrderFlags { get; set; }

        /// <summary>
        /// A list of trade ids related to order (if trades info requested and data available)
        /// </summary>
        public IEnumerable<string> Trades { get; set; }
    }
}