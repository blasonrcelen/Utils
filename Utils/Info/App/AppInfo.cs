using System;

namespace Utils.Info.App
{
    public class Version
    {
        public uint Major;
        public uint Minor;
        public uint Build;
        public uint Revision;

        public Version(uint major = 0, uint minor = 0, uint build = 0, uint revision = 0)
        {
            Major = major;
            Minor = minor;
            Build = build;
            Revision = revision;
        }

        public override string ToString()
        {
            return String.Format("v{0}.{1}.{2}.{3}", Major, Minor, Build, Revision);
        }
    }

    public class AppInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Version Version { get; set; }

        public AppInfo(string name, string description = null, Version version = null)
        {
            Name = name;
            Description = description;
            Version = version == null ? new Version() : version;
        }

        public AppInfo(string name, string description = null, uint major = 0, uint minor = 0, uint build = 0, uint revision = 0)
        {
            Name = name;
            Description = description;
            Version = new Version(major, minor, build, revision);
        }

        public override string ToString() => String.Format("{0} {1}", Name, Version);
    }
}
