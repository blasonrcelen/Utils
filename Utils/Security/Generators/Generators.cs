using System;
using System.Security.Cryptography;
using System.Text;

namespace Utils.Security.Generators
{
    public class Generator
    {
        public static String GetRandomText(int _length, String _allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-?!#=<>&$@%")
        {
            if (_length < 0) throw new ArgumentOutOfRangeException("_length", "length cannot be less than zero.");

            if (String.IsNullOrWhiteSpace(_allowedChars)) throw new ArgumentException("_allowedChars may not be empty.");

            if (_allowedChars.Length > 256) throw new ArgumentException("_allowedChars may contain no more than 256 characters.");

            StringBuilder result = new StringBuilder();
            while (result.Length < _length)
            {
                byte[] buf = GetRandomBytes(128);
                for (int i = 0; i < buf.Length && result.Length < _length; ++i)
                {
                    int outOfRangeStart = 256 - (256 % _allowedChars.Length);
                    if (outOfRangeStart <= buf[i]) continue;

                    result.Append(_allowedChars[buf[i] % _allowedChars.Length]);
                }
            }

            return result.ToString();
        }

        public static byte[] GetRandomBytes(int _length)
        {
            byte[] randomData = new byte[_length];
            new RNGCryptoServiceProvider().GetBytes(randomData);
            return randomData;
        }

        public static byte[] GetDerivationKey(String _key, int _length, int _iterations = 100)
        {
            Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(_key, GetRandomBytes(500));
            return rfc2898.GetBytes(_length);
        }
    }
}
