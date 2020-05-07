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

        public static void Set(String name, String description = null, int major = 0, int minor = 0, int patch = 0)
        {
            Name = name;
            Description = description;
            Major = major;
            Minor = minor;
            Patch = patch;
        }

        public static String GetFullName()
        {
            return Name + " v" + Major + "." + Minor + "." + Patch;
        }
    }
}
