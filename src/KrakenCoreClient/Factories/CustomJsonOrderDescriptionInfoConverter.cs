using System;
using KrakenCoreClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KrakenCoreClient.Factories
{
    public class CustomJsonOrderDescriptionInfoConverter : JsonCreationConverter<OrderDescriptionInfo>
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        protected override OrderDescriptionInfo Create(Type objectType, JArray jArray)
        {
            throw new NotImplementedException();
        }

        protected override OrderDescriptionInfo Create(Type objectType, JObject jObject)
        {
            return jObject.ToObject<OrderDescriptionInfo>();
        }

        protected override OrderDescriptionInfo Create(Type objectType, string text)
        {
            throw new NotImplementedException();
        }
    }
}