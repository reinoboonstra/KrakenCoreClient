using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KrakenCoreClient.Factories
{
    public abstract class JsonCreationConverter<T> : JsonConverter
    {
        /// <summary>
        /// Create an instance of objectType, based properties in the JSON object
        /// </summary>
        /// <param name="objectType">type of object expected</param>
        /// <param name="jArray">
        /// contents of JSON array that will be deserialized
        /// </param>
        /// <returns></returns>
        protected abstract T Create(Type objectType, JArray jArray);

        /// <summary>
        /// Create an instance of objectType, based properties in the JSON object
        /// </summary>
        /// <param name="objectType">type of object expected</param>
        /// <param name="jObject">
        /// contents of JSON object that will be deserialized
        /// </param>
        /// <returns></returns>
        protected abstract T Create(Type objectType, JObject jObject);

        /// <summary>
        /// Create an instance of objectType, based properties in the JSON object
        /// </summary>
        /// <param name="objectType">type of object expected</param>
        /// <param name="text">
        /// contents of text that will be processed
        /// </param>
        /// <returns></returns>
        protected abstract T Create(Type objectType, string text);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            T target = default;

            if (reader.TokenType == JsonToken.StartArray)
            {
                var values = JArray.Load(reader);
                target = Create(objectType, values);
            }

            if (reader.TokenType == JsonToken.StartObject)
            {
                var value = JObject.Load(reader);
                target = Create(objectType, value);
            }

            if (reader.TokenType == JsonToken.String)
            {
                var jToken = JToken.Load(reader);
                target = Create(objectType, jToken.Value<string>());
            }

            return target;
        }
    }
}