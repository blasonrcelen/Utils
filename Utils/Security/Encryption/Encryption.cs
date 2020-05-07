namespace Utils.Security.Encryption
{
    public abstract class Encryption
    {
        protected byte[] key;
        protected byte[] iv;

        public abstract byte[] Key { get; set; }
        public abstract byte[] IV { get; set; }

        public abstract byte[] Encrypt(byte[] data);
        public abstract byte[] Decrypt(byte[] data);
    }
}
