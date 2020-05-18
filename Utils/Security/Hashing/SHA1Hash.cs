using System.Security.Cryptography;

namespace Utils.Security.Hashing
{
    public class SHA1Hash : Hashing
    {
        public SHA1Hash(byte[] data, uint iterations, byte[] salt = null) : base(data, iterations, salt) { }

        protected override byte[] ComputeHash()
        {
            byte[] dataPlusSalt = new byte[Data.Length + Salt.Length];
            Data.CopyTo(dataPlusSalt, 0);
            Salt.CopyTo(dataPlusSalt, Data.Length);

            byte[] hash = SHA1.Create().ComputeHash(dataPlusSalt);
            byte[] stretching = new byte[0];
            for (uint i = Iterations; i > 1; i--)
            {
                stretching = KeyStretching(stretching, hash);
                hash = SHA1.Create().ComputeHash(stretching);
            }
            return hash;
        }
    }
}
