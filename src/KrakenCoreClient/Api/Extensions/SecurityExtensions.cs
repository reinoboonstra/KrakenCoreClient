using System.Security.Cryptography;
using System.Text;

namespace KrakenCoreClient.Api.Extensions
{
    /// <summary>
    /// Security extensions
    /// </summary>
    public static class SecurityExtensions
    {
        /// <summary>
        /// Get Sha 256 hash of given string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] Sha256Hash(this string value)
        {
            using (var sha256Hash = SHA256.Create())
            {
                return sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));
            }
        }

        /// <summary>
        /// Get hmac sha 256 hash of given bytes
        /// </summary>
        /// <param name="keyByte"></param>
        /// <param name="messageBytes"></param>
        /// <returns></returns>
        public static byte[] GetHmacSha512Hash(this byte[] keyByte, byte[] messageBytes)
        {
            using (var hmacSha512 = new HMACSHA512(keyByte))
            {
                return hmacSha512.ComputeHash(messageBytes);
            }
        }
    }
}