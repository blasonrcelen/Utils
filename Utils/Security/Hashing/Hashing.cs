using System;
using Utils.Security.Generators;

namespace Utils.Security.Hashing
{
    public abstract class Hashing : IEquatable<Hashing>
    {
        public readonly byte[] Data, Salt, Hash;
        public readonly uint Iterations;

        public Hashing(byte[] _hashSalt)
        {
            Data = null;
            Iterations = BitConverter.ToUInt32(_hashSalt, 0);
            Salt = new byte[BitConverter.ToUInt32(_hashSalt, 4)];
            Array.Copy(_hashSalt, 8, Salt, 0, BitConverter.ToUInt32(_hashSalt, 4));
            Hash = new byte[_hashSalt.Length - (8 + BitConverter.ToUInt32(_hashSalt, 4))];
            Array.Copy(_hashSalt, 8 + BitConverter.ToUInt32(_hashSalt, 4), Hash, 0, _hashSalt.Length - (8 + BitConverter.ToUInt32(_hashSalt, 4)));
        }

        public Hashing(byte[] _data, uint _iterations, byte[] _salt = null)
        {
            if (_data == null || _data.Length == 0) throw new ArgumentException("_data argument can't be null or empty.");

            if (_iterations == 0) throw new ArgumentException("_iterations can't be zero");

            Data = _data;
            Iterations = _iterations;
            Salt = _salt == null || _salt.Length == 0 ? Generator.GetRandomBytes(500) : _salt;
            Hash = ComputeHash();
        }

        public byte[] GetHashSalt()
        {
            // 4 bytes -> number of iterations
            // 4 bytes -> salt length
            // Salt + Hash
            byte[] combined = new byte[8 + Salt.Length + Hash.Length];
            BitConverter.GetBytes(Iterations).CopyTo(combined, 0);
            BitConverter.GetBytes(Salt.Length).CopyTo(combined, 4);
            Salt.CopyTo(combined, 8);
            Hash.CopyTo(combined, 8 + Salt.Length);
            return combined;
        }

        public bool Equals(Hashing _other)
        {
            return Convert.ToBase64String(Hash) == Convert.ToBase64String(_other.Hash);
        }

        public bool Equals(byte[] _hash)
        {
            return Convert.ToBase64String(Hash) == Convert.ToBase64String(_hash);
        }

        public override bool Equals(object obj)
        {
            return Equals((Hashing)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Hashing _op1, Hashing _op2)
        {
            return _op1.Equals(_op2);
        }

        public static bool operator ==(Hashing _op1, byte _op2)
        {
            return _op1.Equals(_op2);
        }

        public static bool operator ==(byte _op1, Hashing _op2)
        {
            return _op2.Equals(_op1);
        }

        public static bool operator !=(Hashing _op1, Hashing _op2)
        {
            return !_op1.Equals(_op2);
        }

        public static bool operator !=(Hashing _op1, byte _op2)
        {
            return !_op1.Equals(_op2);
        }

        public static bool operator !=(byte _op1, Hashing _op2)
        {
            return !_op2.Equals(_op1);
        }

        public string ToBase64String()
        {
            return Convert.ToBase64String(Hash);
        }

        public string ToHexString()
        {
            return BitConverter.ToString(Hash);
        }

        public override string ToString()
        {
            return ToBase64String();
        }

        protected abstract byte[] ComputeHash();

        protected byte[] keyStretching(byte[] _data, byte[] _hash)
        {
            byte[] stretching = new byte[_data.Length + _hash.Length + Salt.Length];
            _data.CopyTo(stretching, 0);
            _hash.CopyTo(stretching, _data.Length);
            Salt.CopyTo(stretching, _data.Length + _hash.Length);
            return stretching;
        }
    }
}
