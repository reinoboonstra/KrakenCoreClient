using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KrakenCoreClient.Factories
{
    public class CustomJsonListConverter : JsonCreationConverter<IEnumerable<string>>
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        protected override IEnumerable<string> Create(Type objectType, JArray jArray)
        {
            foreach (var jToken in jArray)
            {
                yield return jToken.Value<string>();
            }
        }

        protected override IEnumerable<string> Create(Type objectType, JObject jObject)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<string> Create(Type objectType, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new List<string>();
            }

            return text.Split(",");
        }
    }
}