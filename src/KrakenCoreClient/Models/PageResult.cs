using System.Collections.Generic;

namespace KrakenCoreClient.Models
{
    public class PageResult<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();

        public long Count { get; set; } = 0;
    }
}