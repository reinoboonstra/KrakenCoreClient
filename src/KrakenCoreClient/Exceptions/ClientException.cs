using System;
using System.Net;
using System.Net.Http;

namespace KrakenCoreClient.Exceptions
{
    public class ClientException : HttpRequestException
    {
        public ClientException(string message) : base(message)
        {
        }

        public ClientException(string message, Exception inner) : base(message, inner)
        {
        }

        public HttpStatusCode HttpStatusCode { get; set; }

        public string ApiFaultMessage { get; set; }
    }
}