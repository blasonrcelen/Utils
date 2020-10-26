using System;
using System.IO;
using System.Security.Cryptography;
using Utils.Security.Generators;

namespace Utils.Security.Encryption
{
    public class DES : Encryption
    {
        public const int KEYLENGTH = 8;
        public const int IVLENGTH = 8;

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

        public DES(byte[] key = null, byte[] iv = null)
        {
            if (key == null) key = Generator.GetRandomBytes(KEYLENGTH);
            if (iv == null) iv = Generator.GetRandomBytes(IVLENGTH);

            Key = key;
            IV = iv;
        }

        public override byte[] Decrypt(byte[] data)
        {
            System.Security.Cryptography.DES des = System.Security.Cryptography.DES.Create();
            des.Key = Key;
            des.IV = IV;

            byte[] decryptedData;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                    cs.Close();
                }
                decryptedData = ms.ToArray();
            }
            des.Clear();
            return decryptedData;
        }

        public override byte[] Encrypt(byte[] data)
        {
            System.Security.Cryptography.DES des = System.Security.Cryptography.DES.Create();
            des.Key = Key;
            des.IV = IV;

            byte[] encryptedData;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                    cs.Close();
                }
                encryptedData = ms.ToArray();
            }
            des.Clear();
            return encryptedData;
        }
    }
}
