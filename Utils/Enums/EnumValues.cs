using System;
using System.Reflection;

namespace Utils.Enums
{
    public class EnumValue : Attribute
    {
        public readonly object Value;

        public EnumValue(object value) => Value = value;

        public static char GetChar(Enum _enum) =>
            _enum.GetType().GetField(_enum.ToString()).GetCustomAttribute<EnumValue>(false) is EnumValue enumValue ? (char)enumValue.Value : '\0';

        public static string GetString(Enum _enum) =>
            _enum.GetType().GetField(_enum.ToString()).GetCustomAttribute<EnumValue>(false) is EnumValue enumValue ? (string)enumValue.Value : null;

        public static byte GetByte(Enum _enum) =>
            _enum.GetType().GetField(_enum.ToString()).GetCustomAttribute<EnumValue>(false) is EnumValue enumValue ? Convert.ToByte(enumValue.Value) : (byte)0x00;

        public static int GetInt(Enum _enum) =>
            _enum.GetType().GetField(_enum.ToString()).GetCustomAttribute<EnumValue>(false) is EnumValue enumValue ? Convert.ToInt32(enumValue.Value) : 0;

        public static long GetLong(Enum _enum) =>
            _enum.GetType().GetField(_enum.ToString()).GetCustomAttribute<EnumValue>(false) is EnumValue enumValue ? Convert.ToInt64(enumValue.Value) : 0L;

        public static double GetDouble(Enum _enum) =>
            _enum.GetType().GetField(_enum.ToString()).GetCustomAttribute<EnumValue>(false) is EnumValue enumValue ? Convert.ToDouble(enumValue.Value) : 0D;

        public static decimal GetDecimal(Enum _enum) =>
            _enum.GetType().GetField(_enum.ToString()).GetCustomAttribute<EnumValue>(false) is EnumValue enumValue ? Convert.ToDecimal(enumValue.Value) : 0M;

        public static bool GetBoolean(Enum _enum) =>
            _enum.GetType().GetField(_enum.ToString()).GetCustomAttribute<EnumValue>(false) is EnumValue enumValue ? Convert.ToBoolean(enumValue) : false;

        public static object GetObject(Enum _enum) =>
            _enum.GetType().GetField(_enum.ToString()).GetCustomAttribute<EnumValue>(false) is EnumValue enumValue ? enumValue.Value : null;

        public static T GetObject<T>(Enum _enum) => (T)GetObject(_enum);
    }

    public static class EnumMethods
    {
        public static char GetChar(this Enum _enum) => EnumValue.GetChar(_enum);
        public static string GetString(this Enum _enum) => EnumValue.GetString(_enum);
        public static byte GetByte(this Enum _enum) => EnumValue.GetByte(_enum);
        public static int GetInt(this Enum _enum) => EnumValue.GetInt(_enum);
        public static long GetLong(this Enum _enum) => EnumValue.GetLong(_enum);
        public static double GetDouble(this Enum _enum) => EnumValue.GetDouble(_enum);
        public static decimal GetDecimal(this Enum _enum) => EnumValue.GetDecimal(_enum);
        public static bool GetBoolean(this Enum _enum) => EnumValue.GetBoolean(_enum);
        public static object GetObject(this Enum _enum) => EnumValue.GetObject(_enum);
        public static T GetObject<T>(this Enum _enum) => EnumValue.GetObject<T>(_enum);
    }
}
