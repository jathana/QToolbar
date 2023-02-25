namespace QToolbar
{
   /// <summary>
   /// Byte array extensions
   /// </summary>
   public static class ByteArrayExtensions
   {
      private static readonly uint[] _lookup32 = CreateLookup32();

      private static uint[] CreateLookup32()
      {
         uint[] result = new uint[256];
         for (int i = 0; i < 256; i++)
         {
            string s = i.ToString("X2");
            result[i] = s[0] + ((uint)s[1] << 16);
         }
         return result;
      }

      private static string ByteArrayToHexViaLookup32(byte[] bytes)
      {
         uint[] lookup32 = _lookup32;
         char[] result = new char[bytes.Length * 2];
         for (int i = 0; i < bytes.Length; i++)
         {
            uint val = lookup32[bytes[i]];
            result[2 * i] = (char)val;
            result[2 * i + 1] = (char)(val >> 16);
         }
         return new string(result);
      }

      /// <summary>
      /// Converts byte array to hex string representation
      /// </summary>
      /// <param name="bytes">Byte data to convert</param>
      /// <returns>Hex representation of the byte data.</returns>
      public static string ToHexString(this byte[] bytes) => ByteArrayToHexViaLookup32(bytes);
   }
}
