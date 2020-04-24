using System.Security.Cryptography;

namespace Utils.Security.Hashing
{
    public class MD5Hash : Hashing
    {
        public MD5Hash(byte[] _hashSalt) : base(_hashSalt) { }
        public MD5Hash(byte[] _data, uint _iterations, byte[] _salt = null) : base(_data, _iterations, _salt) { }

        protected override byte[] ComputeHash()
        {
            byte[] dataPlusSalt = new byte[Data.Length + Salt.Length];
            Data.CopyTo(dataPlusSalt, 0);
            Salt.CopyTo(dataPlusSalt, Data.Length);

            byte[] hash = MD5.Create().ComputeHash(dataPlusSalt);
            byte[] stretching = new byte[0];
            for (uint i = Iterations; i > 1; i--)
            {
                stretching = keyStretching(stretching, hash);
                hash = MD5.Create().ComputeHash(stretching);
            }
            return hash;
        }
    }
}
