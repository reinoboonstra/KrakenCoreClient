using System;
using System.Collections.Generic;
using System.Linq;
using KrakenCoreClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KrakenCoreClient.Factories
{
    public class CustomJsonOhlcDataConverter : JsonCreationConverter<OhlcData>
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        protected override OhlcData Create(Type objectType, JArray jArray)
        {
            throw new NotImplementedException();
        }

        protected override OhlcData Create(Type objectType, JObject jObject)
        {
            return new OhlcData
            {
                Pair = jObject.First.Path,
                Entries = GetEntries(jObject.First.First),
                Last = Convert.ToInt64(((JProperty)jObject.Last).Value)
            };
        }

        protected override OhlcData Create(Type objectType, string text)
        {
            throw new NotImplementedException();
        }

        #region Private methods

        private static IEnumerable<OhlcEntry> GetEntries(JToken token)
        {
            return token.Select(jToken => new OhlcEntry
            {
                Time = Convert.ToInt64(jToken[0]),
                Open = Convert.ToDecimal(jToken[1]),
                High = Convert.ToDecimal(jToken[2]),
                Low = Convert.ToDecimal(jToken[3]),
                Close = Convert.ToDecimal(jToken[4]),
                VSwap = Convert.ToDecimal(jToken[5]),
                Volume = Convert.ToDecimal(jToken[6]),
                Count = Convert.ToInt64(jToken[7])
            });
        }

        #endregion
    }
}