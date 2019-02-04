using System;
using KrakenCoreClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KrakenCoreClient.Factories
{
    public class CustomJsonTickerInformationConverter : JsonCreationConverter<TickerInformation>
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        protected override TickerInformation Create(Type objectType, JArray jArray)
        {
            throw new NotImplementedException();
        }

        protected override TickerInformation Create(Type objectType, JObject jObject)
        {
            return new TickerInformation
            {
                Ask = GetAskBid(jObject["a"]),
                Bid = GetAskBid(jObject["b"]),
                LastTradeClosed = GetLastTradeClosed(jObject["c"]),
                Volume = GetRecent(jObject["v"]),
                VolumeWeightedAveragePrice = GetRecent(jObject["p"]),
                NumberOfTrades = GetRecent(jObject["t"]),
                Low = GetRecent(jObject["l"]),
                High = GetRecent(jObject["h"]),
                OpeningPriceToday = Convert.ToDecimal(jObject["o"])
            };
        }

        protected override TickerInformation Create(Type objectType, string text)
        {
            throw new NotImplementedException();
        }

        #region Private methods

        private static AskBid GetAskBid(JToken token)
        {
            return new AskBid
            {
                Price = Convert.ToDecimal(token[0]),
                WholeLotVolume = Convert.ToDecimal(token[1]),
                LotVolume = Convert.ToDecimal(token[2])
            };
        }

        private static LastTradeClosed GetLastTradeClosed(JToken token)
        {
            return new LastTradeClosed
            {
                Price = Convert.ToDecimal(token[0]),
                LotVolume = Convert.ToDecimal(token[1])
            };
        }

        private static Recent GetRecent(JToken token)
        {
            return new Recent
            {
                Today = Convert.ToDecimal(token[0]),
                Last24Hours = Convert.ToDecimal(token[1])
            };
        }

        #endregion
    }
}