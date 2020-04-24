namespace Utils.Security.Encryption
{
    public abstract class Encryption
    {
        protected byte[] key;
        protected byte[] iv;

        public abstract byte[] Key { get; set; }
        public abstract byte[] IV { get; set; }

        public abstract byte[] Encrypt(byte[] _data);
        public abstract byte[] Decrypt(byte[] _data);

        public byte[] GetKeyIv()
        {
            byte[] keyIV = new byte[Key.Length + IV.Length];
            Key.CopyTo(keyIV, 0);
            IV.CopyTo(keyIV, Key.Length);
            return keyIV;
        }
    }
}
