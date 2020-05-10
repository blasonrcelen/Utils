using System;

namespace Utils.Info
{
    public class AppInfo
    {
        public String Name { get; private set; }
        public String Description { get; private set; }

        public int Major { get; private set; }
        public int Minor { get; private set; }
        public int Patch { get; private set; }

        public AppInfo(String name, String description = null, int major = 0, int minor = 0, int patch = 0)
        {
            Name = name;
            Description = description;
            Major = major;
            Minor = minor;
            Patch = patch;
        }

        public String GetFullName()
        {
            return Name + " v" + Major + "." + Minor + "." + Patch;
        }
    }
}
