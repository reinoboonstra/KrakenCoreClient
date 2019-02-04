namespace KrakenCoreClient.Models
{
    public class ServerTime
    {
        /// <summary>
        /// Unix time
        /// </summary>
        public long UnixTime { get; set; }

        /// <summary>
        /// RFC 1123
        /// </summary>
        public string Rfc1123 { get; set; }
    }
}