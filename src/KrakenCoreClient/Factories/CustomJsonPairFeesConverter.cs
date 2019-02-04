using System;
using System.Collections.Generic;
using KrakenCoreClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KrakenCoreClient.Factories
{
    public class CustomJsonPairFeesConverter : JsonCreationConverter<IEnumerable<PairFees>>
    {
        private readonly JsonSerializer jsonSerializer = new JsonSerializer
        {
            ContractResolver = new CustomContractResolver()
        };

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        protected override IEnumerable<PairFees> Create(Type objectType, JArray jArray)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<PairFees> Create(Type objectType, JObject jObject)
        {
            foreach (var pair in jObject)
            {
                var pairVolume = pair.Value.ToObject<PairFees>(jsonSerializer);
                pairVolume.Pair = pair.Key;

                yield return pairVolume;
            }
        }

        protected override IEnumerable<PairFees> Create(Type objectType, string text)
        {
            throw new NotImplementedException();
        }
    }
}