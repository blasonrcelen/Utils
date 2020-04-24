using System;
using System.Reflection;

namespace Utils.Enums
{
    public class EnumValue : Attribute
    {
        public readonly object Value;

        public EnumValue(object _value)
        {
            Value = _value;
        }

        public static String GetString(Enum _enum)
        {
            EnumValue enumValue = _enum.GetType().GetField(_enum.ToString()).GetCustomAttribute<EnumValue>(false) as EnumValue;
            return enumValue != null ? (String)enumValue.Value : null;
        }

        public static byte GetByte(Enum _enum)
        {
            EnumValue enumValue = _enum.GetType().GetField(_enum.ToString()).GetCustomAttribute<EnumValue>(false) as EnumValue;
            return enumValue != null ? Convert.ToByte(enumValue.Value) : (byte)0x00;
        }

        public static int GetInt(Enum _enum)
        {
            EnumValue enumValue = _enum.GetType().GetField(_enum.ToString()).GetCustomAttribute<EnumValue>(false) as EnumValue;
            return enumValue != null ? Convert.ToInt32(enumValue.Value) : 0;
        }

        public static long GetLong(Enum _enum)
        {
            EnumValue enumValue = _enum.GetType().GetField(_enum.ToString()).GetCustomAttribute<EnumValue>(false) as EnumValue;
            return enumValue != null ? Convert.ToInt64(enumValue.Value) : 0L;
        }

        public static double GetDouble(Enum _enum)
        {
            EnumValue enumValue = _enum.GetType().GetField(_enum.ToString()).GetCustomAttribute<EnumValue>(false) as EnumValue;
            return enumValue != null ? Convert.ToDouble(enumValue.Value) : 0D;
        }

        public static decimal GetDecimal(Enum _enum)
        {
            EnumValue enumValue = _enum.GetType().GetField(_enum.ToString()).GetCustomAttribute<EnumValue>(false) as EnumValue;
            return enumValue != null ? Convert.ToDecimal(enumValue.Value) : 0M;
        }

        public static bool GetBoolean(Enum _enum)
        {
            EnumValue enumValue = _enum.GetType().GetField(_enum.ToString()).GetCustomAttribute<EnumValue>(false) as EnumValue;
            return enumValue != null ? Convert.ToBoolean(enumValue) : false;
        }
    }

    public static class EnumMethods
    {
        public static String GetString(this Enum _enum)
        {
            return EnumValue.GetString(_enum);
        }

        public static byte GetByte(this Enum _enum)
        {
            return EnumValue.GetByte(_enum);
        }

        public static int GetInt(this Enum _enum)
        {
            return EnumValue.GetInt(_enum);
        }

        public static long GetLong(this Enum _enum)
        {
            return EnumValue.GetLong(_enum);
        }

        public static double GetDouble(this Enum _enum)
        {
            return EnumValue.GetDouble(_enum);
        }

        public static decimal GetDecimal(this Enum _enum)
        {
            return EnumValue.GetDecimal(_enum);
        }

        public static bool GetBoolean(this Enum _enum)
        {
            return EnumValue.GetBoolean(_enum);
        }
    }
}
