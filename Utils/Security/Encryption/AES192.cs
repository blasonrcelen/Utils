using System;
using System.IO;
using System.Security.Cryptography;
using Utils.Security.Generators;

namespace Utils.Security.Encryption
{
    public class AES192 : Encryption
    {
        public const int KEY_LENGTH = 24;
        public const int IV_LENGTH = 16;

        public override byte[] Key
        {
            get { return key; }
            set
            {
                if (value.Length != KEY_LENGTH)
                    throw new Exception($"Key must be {KEY_LENGTH} bytes length.");
                key = value;
            }
        }

        public override byte[] IV
        {
            get { return iv; }
            set
            {
                if (value.Length != IV_LENGTH)
                    throw new Exception($"IV must be {IV_LENGTH} bytes length.");
                iv = value;
            }
        }

        public AES192(byte[] _combinedKeyIV)
        {
            if (_combinedKeyIV == null) throw new ArgumentNullException("Argument _combinedKeyIV can't be null.");
            if (_combinedKeyIV.Length != KEY_LENGTH + IV_LENGTH) throw new ArgumentException($"Key and Iv combination must have to be {KEY_LENGTH + IV_LENGTH} byte length.");

            byte[] key = new byte[KEY_LENGTH];
            byte[] iv = new byte[IV_LENGTH];

            Array.Copy(_combinedKeyIV, 0, key, 0, KEY_LENGTH);
            Array.Copy(_combinedKeyIV, KEY_LENGTH, iv, 0, IV_LENGTH);

            Key = key;
            IV = iv;
        }

        public AES192(byte[] _key = null, byte[] _iv = null)
        {
            if (_key == null) _key = Generator.GetRandomBytes(KEY_LENGTH);
            if (_iv == null) _iv = Generator.GetRandomBytes(IV_LENGTH);

            Key = _key;
            IV = _iv;
        }

        public override byte[] Decrypt(byte[] _data)
        {
            Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            byte[] decryptedData;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(_data, 0, _data.Length);
                    cs.Close();
                }
                decryptedData = ms.ToArray();
            }
            aes.Clear();
            return decryptedData;
        }

        public override byte[] Encrypt(byte[] _data)
        {
            Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            byte[] decryptedData;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(_data, 0, _data.Length);
                    cs.Close();
                }
                decryptedData = ms.ToArray();
            }
            aes.Clear();
            return decryptedData;
        }
    }
}
