using System;
using System.Collections.Generic;
using KrakenCoreClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KrakenCoreClient.Factories
{
    public class CustomJsonFeesConverter : JsonCreationConverter<IEnumerable<Fee>>
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        protected override IEnumerable<Fee> Create(Type objectType, JArray jArray)
        {
            foreach (var jToken in jArray)
            {
                var fee = (JArray) jToken;

                yield return new Fee
                {
                    Volume = Convert.ToInt32(fee[0]),
                    Percentage = Convert.ToDecimal(fee[1])
                };
            }
        }

        protected override IEnumerable<Fee> Create(Type objectType, JObject jObject)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<Fee> Create(Type objectType, string text)
        {
            throw new NotImplementedException();
        }
    }
}