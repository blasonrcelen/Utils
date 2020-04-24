using System;

namespace Utils.Info
{
    public class AppInfo
    {
        public static String Name { get; private set; }
        public static String Description { get; private set; }

        public static int Major { get; private set; }
        public static int Minor { get; private set; }
        public static int Patch { get; private set; }

        public static void Set(String _name, String _description = null, int _major = 0, int _minor = 0, int _patch = 0)
        {
            Name = _name;
            Description = _description;
            Major = _major;
            Minor = _minor;
            Patch = _patch;
        }

        public static String GetFullName()
        {
            return Name + " v" + Major + "." + Minor + "." + Patch;
        }
    }
}
