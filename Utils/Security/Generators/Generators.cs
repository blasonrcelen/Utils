using System;
using System.Security.Cryptography;
using System.Text;

namespace Utils.Security.Generators
{
    public class Generator
    {
        public static String GetRandomText(int length, String allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-?!#=<>&$@%")
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "length cannot be less than zero.");

            if (String.IsNullOrWhiteSpace(allowedChars)) throw new ArgumentException("allowedChars may not be empty.");

            if (allowedChars.Length > 256) throw new ArgumentException("allowedChars may contain no more than 256 characters.");

            StringBuilder result = new StringBuilder();
            while (result.Length < length)
            {
                byte[] buf = GetRandomBytes(128);
                for (int i = 0; i < buf.Length && result.Length < length; ++i)
                {
                    int outOfRangeStart = 256 - (256 % allowedChars.Length);
                    if (outOfRangeStart <= buf[i]) continue;

                    result.Append(allowedChars[buf[i] % allowedChars.Length]);
                }
            }

            return result.ToString();
        }

        public static byte[] GetRandomBytes(int length)
        {
            byte[] randomData = new byte[length];
            new RNGCryptoServiceProvider().GetBytes(randomData);
            return randomData;
        }

        public static byte[] GetDerivationKey(String key, int length, int iterations = 100)
        {
            Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(key, GetRandomBytes(500));
            return rfc2898.GetBytes(length);
        }
    }
}
