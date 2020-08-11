
namespace Utils.Info.App
{
    public class AppInfo
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public int Major { get; private set; }
        public int Minor { get; private set; }
        public int Patch { get; private set; }

        public AppInfo(string name, string description = null, int major = 0, int minor = 0, int patch = 0)
        {
            Name = name;
            Description = description;
            Major = major;
            Minor = minor;
            Patch = patch;
        }

        public string GetFullName() => Name + " v" + Major + "." + Minor + "." + Patch;
    }
}
