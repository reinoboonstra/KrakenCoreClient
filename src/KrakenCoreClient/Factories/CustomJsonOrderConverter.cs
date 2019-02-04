using System;
using System.Collections.Generic;
using KrakenCoreClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KrakenCoreClient.Factories
{
    public class CustomJsonOrderConverter : JsonCreationConverter<IEnumerable<BookEntry>>
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        protected override IEnumerable<BookEntry> Create(Type objectType, JArray jArray)
        {
            foreach (var jToken in jArray)
            {
                var order = (JArray) jToken;

                yield return new BookEntry
                {
                    Price = Convert.ToDecimal(order[0]),
                    Volume = Convert.ToDecimal(order[1]),
                    Time = Convert.ToInt64(order[2])
                };
            }
        }

        protected override IEnumerable<BookEntry> Create(Type objectType, JObject jObject)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<BookEntry> Create(Type objectType, string text)
        {
            throw new NotImplementedException();
        }
    }
}