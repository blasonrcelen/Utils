using System.Security.Cryptography;

namespace Utils.Security.Hashing
{
    public class SHA512Hash : Hashing
    {
        public SHA512Hash(byte[] _hashSalt) : base(_hashSalt) { }
        public SHA512Hash(byte[] _data, uint _iterations, byte[] _salt = null) : base(_data, _iterations, _salt) { }

        protected override byte[] ComputeHash()
        {
            byte[] dataPlusSalt = new byte[Data.Length + Salt.Length];
            Data.CopyTo(dataPlusSalt, 0);
            Salt.CopyTo(dataPlusSalt, Data.Length);

            byte[] hash = SHA512.Create().ComputeHash(dataPlusSalt);
            byte[] stretching = new byte[0];
            for (uint i = Iterations; i > 1; i--)
            {
                stretching = keyStretching(stretching, hash);
                hash = SHA512.Create().ComputeHash(stretching);
            }
            return hash;
        }
    }
}
