using System;
using Utils.Security.Generators;

namespace Utils.Security.Hashing
{
    public abstract class Hashing : IEquatable<Hashing>
    {
        public readonly byte[] Data, Salt, Hash;
        public readonly uint Iterations;

        public Hashing(byte[] data, uint iterations, byte[] salt = null)
        {
            if (data == null || data.Length == 0) throw new ArgumentException("data argument can't be null or empty.");
            if (iterations == 0) throw new ArgumentException("iterations can't be zero");

            Data = data;
            Iterations = iterations;
            Salt = salt == null || salt.Length == 0 ? Generator.GetRandomBytes(500) : salt;
            Hash = ComputeHash();
        }

        public bool Equals(Hashing other) => Convert.ToBase64String(Hash) == Convert.ToBase64String(other.Hash);

        public bool Equals(byte[] hash) => Convert.ToBase64String(Hash) == Convert.ToBase64String(hash);

        public override bool Equals(object obj) => Equals((Hashing)obj);

        public override int GetHashCode() => base.GetHashCode();

        public static bool operator ==(Hashing op1, Hashing op2) => op1.Equals(op2);

        public static bool operator ==(Hashing op1, byte[] op2) => op1.Equals(op2);

        public static bool operator ==(byte[] op1, Hashing op2) => op2.Equals(op1);

        public static bool operator !=(Hashing op1, Hashing op2) => !op1.Equals(op2);

        public static bool operator !=(Hashing op1, byte[] op2) => !op1.Equals(op2);

        public static bool operator !=(byte[] op1, Hashing op2) => !op2.Equals(op1);

        public string ToBase64String() => Convert.ToBase64String(Hash);

        public string ToHexString() => BitConverter.ToString(Hash);

        public override string ToString() => ToBase64String();

        protected abstract byte[] ComputeHash();

        protected byte[] KeyStretching(byte[] data, byte[] hash)
        {
            byte[] stretching = new byte[data.Length + hash.Length + Salt.Length];
            data.CopyTo(stretching, 0);
            hash.CopyTo(stretching, data.Length);
            Salt.CopyTo(stretching, data.Length + hash.Length);
            return stretching;
        }
    }
}
