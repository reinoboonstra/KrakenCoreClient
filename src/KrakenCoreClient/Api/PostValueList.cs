using System.Collections.Generic;
using KrakenCoreClient.Models;

namespace KrakenCoreClient.Api
{
    public class PostValueList
    {
        private readonly Dictionary<string, object> postValues;
            
        public PostValueList() 
        {
            postValues = new Dictionary<string, object>();
        }
            
        public void Add(string key, object value)
        {
            if (value == null)
            {
                return;
            }

            var parameterValue = value is IEnumerable<string> values
                ? string.Join(",", values)
                : value;

            postValues.Add(key, parameterValue);
        }

        public override string ToString()
        {
            var parameters = string.Empty;

            foreach (var (key, value) in postValues)
            {
                if (value == null)
                {
                    continue;
                }

                var elementValue = ToStringValue(value);

                if (string.IsNullOrEmpty(elementValue))
                {
                    continue;
                }

                parameters += parameters == string.Empty ? string.Empty : "&";
                parameters += $"{key}={elementValue}";
            }

            return parameters;
        }

        #region Private methods

        /// <summary>
        /// Convert object, which should be a Kraken type, to a corresponding string value
        /// </summary>
        /// <param name="value">The object</param>
        /// <returns>String representation of the object</returns>
        private static string ToStringValue(object value)
        {
            if (value is TradeType type)
            {
                switch (type)
                {
                    case TradeType.AnyPosition: return "any position";
                    case TradeType.ClosedPosition: return "closed position";
                    case TradeType.ClosingPosition: return "closing position";
                    case TradeType.NoPosition: return "no position";
                    default: return "all";
                }
            }

            if (value is IEnumerable<string> list)
            {
                return string.Join(",", list);
            }

            if (value is bool ||
                value is CloseTime ||
                value is AssetClass ||
                value is AssetInfo ||
                value is TradableAssetPairInfo ||
                value is LedgerType ||
                value is BuyOrSell ||
                value is OrderType)
            {
                return value.ToString().ToLower();
            }

            return value.ToString();
        }

        #endregion
    }
}