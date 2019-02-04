using System.Collections.Generic;

namespace KrakenCoreClient.Models
{
    public class OhlcData
    {
        /// <summary>
        /// Pair name
        /// </summary>
        public string Pair { get; set; }

        /// <summary>
        /// Array of entries(<time>, <open>, <high>, <low>, <close>, <vwap>, <volume>, <count>)
        /// </summary>
        public IEnumerable<OhlcEntry> Entries { get; set; }

        /// <summary>
        /// Id to be used as since when polling for new, committed OHLC data
        /// </summary>
        public long Last { get; set; }
    }
}