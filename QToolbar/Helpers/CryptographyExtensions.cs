using System;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;



namespace QToolbar
{
   /// <summary>
   /// Cryptographic extensions
   /// </summary>
   public static class CryptographyExtensions
   {
      /// <summary>
      /// Computes SHA256 hash and returns it as base 64 string
      /// </summary>
      /// <param name="sourceString">String to hash.</param>
      /// <returns>Base64 string representing the hash.</returns>
      public static string GetSHA256HashBase64(this string sourceString) => CalculateHashBase64<Sha256Digest>(sourceString);

      /// <summary>
      /// Computes SHA256 hash and returns it as hex string
      /// </summary>
      /// <param name="sourceString">String to hash.</param>
      /// <returns>HEX string representing the hash.</returns>
      public static string GetSHA256HashHex(this string sourceString) => CalculateHashHex<Sha256Digest>(sourceString);

      /// <summary>
      /// Computes SHA1 hash and returns it as base 64 string
      /// </summary>
      /// <param name="sourceString">String to hash.</param>
      /// <returns>Base64 string representing the hash.</returns>
      public static string GetSHA1HashBase64(this string sourceString) => CalculateHashBase64<Sha1Digest>(sourceString);

      /// <summary>
      /// Computes SHA1 hash and returns it as hex string
      /// </summary>
      /// <param name="sourceString">String to hash.</param>
      /// <returns>HEX string representing the hash.</returns>
      public static string GetSHA1HashHex(this string sourceString) => CalculateHashHex<Sha1Digest>(sourceString);

      /// <summary>
      /// Computes MD5 hash and returns it as base 64 string
      /// </summary>
      /// <param name="sourceString">String to hash.</param>
      /// <returns>Base64 string representing the hash.</returns>
      public static string GetMD5HashBase64(this string sourceString) => CalculateHashBase64<MD5Digest>(sourceString);

      /// <summary>
      /// Computes MD5 hash and returns it as hex string
      /// </summary>
      /// <param name="sourceString">String to hash.</param>
      /// <returns>HEX string representing the hash.</returns>
      public static string GetMD5HashHex(this string sourceString) => CalculateHashHex<MD5Digest>(sourceString);

      private static byte[] CalculateHash<T>(byte[] data) where T : IDigest, new()
      {
         IDigest digest = new T();
         digest.BlockUpdate(data, 0, data.Length);
         byte[] hashBytes = new byte[digest.GetDigestSize()];
         digest.DoFinal(hashBytes, 0);
         return hashBytes;
      }

      private static byte[] CalculateHash<T>(string dataString) where T : IDigest, new() => CalculateHash<T>(Encoding.UTF8.GetBytes(dataString));

      private static string CalculateHashHex<T>(string dataString) where T : IDigest, new() => CalculateHash<T>(dataString).ToHexString();

      private static string CalculateHashBase64<T>(string dataString) where T : IDigest, new() => Convert.ToBase64String(CalculateHash<T>(dataString));
   }
}
