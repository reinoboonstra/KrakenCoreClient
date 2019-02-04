using System;
using System.Collections.Generic;
using System.Linq;
using KrakenCoreClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KrakenCoreClient.Factories
{
    public class CustomJsonRecentTradesConverter : JsonCreationConverter<RecentTrades>
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        protected override RecentTrades Create(Type objectType, JArray jArray)
        {
            throw new NotImplementedException();
        }

        protected override RecentTrades Create(Type objectType, JObject jObject)
        {
            return new RecentTrades
            {
                Pair = jObject.First.Path,
                Trades = GetTrades(jObject.First.First),
                Last = Convert.ToInt64(((JProperty)jObject.Last).Value)
            };
        }

        protected override RecentTrades Create(Type objectType, string text)
        {
            throw new NotImplementedException();
        }

        #region Private methods

        private static IEnumerable<TradeEntry> GetTrades(JToken token)
        {
            return token.Select(jToken => new TradeEntry
            {
                Price = Convert.ToDecimal(jToken[0]),
                Volume = Convert.ToDecimal(jToken[1]),
                Time = Convert.ToInt64(jToken[2]),
                BuySell = GetBuyOrSell(jToken[3].ToString()),
                MarketLimit = GetMarketOrLimit(jToken[4].ToString()),
                Miscellaneous = jToken[5].ToString()
            });
        }

        public static BuyOrSell GetBuyOrSell(string buyOrSell)
        {
            return buyOrSell.ToLower() == "b" ? BuyOrSell.Buy : BuyOrSell.Sell;
        }

        public static MarketOrLimit GetMarketOrLimit(string marketOrLimit)
        {
            return marketOrLimit.ToLower() == "m" ? MarketOrLimit.Market : MarketOrLimit.Limit;
        }

        #endregion
    }
}