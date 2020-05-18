using System;
using System.IO;
using System.Security.Cryptography;
using Utils.Security.Generators;

namespace Utils.Security.Encryption
{
    public class AES128 : Encryption
    {
        public const int KEYLENGTH = 16;
        public const int IVLENGTH = 16;

        public override byte[] Key
        {
            get => key;
            set => key = value.Length != KEYLENGTH ? throw new Exception($"Key must be {KEYLENGTH} bytes length.") : value;
        }

        public override byte[] IV
        {
            get => iv;
            set => iv = value.Length != IVLENGTH ? throw new Exception($"IV must be {IVLENGTH} bytes length.") : value;
        }

        public AES128(byte[] key = null, byte[] iv = null)
        {
            if (key == null) key = Generator.GetRandomBytes(KEYLENGTH);
            if (iv == null) iv = Generator.GetRandomBytes(IVLENGTH);

            Key = key;
            IV = iv;
        }

        public override byte[] Decrypt(byte[] data)
        {
            Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            byte[] decryptedData;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                    cs.Close();
                }
                decryptedData = ms.ToArray();
            }
            aes.Clear();
            return decryptedData;
        }

        public override byte[] Encrypt(byte[] data)
        {
            Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            byte[] decryptedData;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                    cs.Close();
                }
                decryptedData = ms.ToArray();
            }
            aes.Clear();
            return decryptedData;
        }
    }
}
