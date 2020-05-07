using System.Security.Cryptography;

namespace Utils.Security.Hashing
{
    public class SHA384Hash : Hashing
    {
        public SHA384Hash(byte[] data, uint iterations, byte[] salt = null) : base(data, iterations, salt) { }

        protected override byte[] ComputeHash()
        {
            byte[] dataPlusSalt = new byte[Data.Length + Salt.Length];
            Data.CopyTo(dataPlusSalt, 0);
            Salt.CopyTo(dataPlusSalt, Data.Length);

            byte[] hash = SHA384.Create().ComputeHash(dataPlusSalt);
            byte[] stretching = new byte[0];
            for (uint i = Iterations; i > 1; i--)
            {
                stretching = keyStretching(stretching, hash);
                hash = SHA384.Create().ComputeHash(stretching);
            }
            return hash;
        }
    }
}
