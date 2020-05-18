using System.Security.Cryptography;

namespace Utils.Security.Hashing
{
    public class MD5Hash : Hashing
    {
        public MD5Hash(byte[] data, uint iterations, byte[] salt = null) : base(data, iterations, salt) { }

        protected override byte[] ComputeHash()
        {
            byte[] dataPlusSalt = new byte[Data.Length + Salt.Length];
            Data.CopyTo(dataPlusSalt, 0);
            Salt.CopyTo(dataPlusSalt, Data.Length);

            byte[] hash = MD5.Create().ComputeHash(dataPlusSalt);
            byte[] stretching = new byte[0];
            for (uint i = Iterations; i > 1; i--)
            {
                stretching = KeyStretching(stretching, hash);
                hash = MD5.Create().ComputeHash(stretching);
            }
            return hash;
        }
    }
}
