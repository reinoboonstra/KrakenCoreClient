using System.Collections.Generic;
using System.Linq;

namespace KrakenCoreClient.Api
{
    public class QueryStringList
    {
        private readonly Dictionary<string, string> queryParameters;
            
        public QueryStringList() 
        {
            queryParameters = new Dictionary<string, string>();
        }
            
        public void Add(string key, object value)
        {
            if (value == null)
            {
                return;
            }

            var parameterValue = value is IEnumerable<string> values
                ? string.Join(",", values)
                : value.ToString();

            queryParameters.Add(key, parameterValue.ToLower());
        }

        public override string ToString()
        {
            return !queryParameters.Any()
                ? string.Empty
                : $"?{string.Join("&", queryParameters.Select(kvp => $"{kvp.Key}={kvp.Value}"))}";
        }
    }
}