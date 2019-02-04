using System;
using System.Collections.Generic;
using System.Linq;
using KrakenCoreClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KrakenCoreClient.Factories
{
    public class CustomJsonRecentSpreadDataConverter : JsonCreationConverter<RecentSpreadData>
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        protected override RecentSpreadData Create(Type objectType, JArray jArray)
        {
            throw new NotImplementedException();
        }

        protected override RecentSpreadData Create(Type objectType, JObject jObject)
        {
            return new RecentSpreadData
            {
                Pair = jObject.First.Path,
                Entries = GetEntries(jObject.First.First),
                Last = Convert.ToInt64(((JProperty)jObject.Last).Value)
            };
        }

        protected override RecentSpreadData Create(Type objectType, string text)
        {
            throw new NotImplementedException();
        }

        #region Private methods

        private static IEnumerable<SpreadEntry> GetEntries(JToken token)
        {
            return token.Select(jToken => new SpreadEntry
            {
                Time = Convert.ToInt64(jToken[0]),
                Bid = Convert.ToDecimal(jToken[1]),
                Ask = Convert.ToDecimal(jToken[2])
            });
        }

        #endregion
    }
}