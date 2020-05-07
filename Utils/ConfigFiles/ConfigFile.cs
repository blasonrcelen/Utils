using System;
using System.IO;

namespace Utils.ConfigFiles
{
    public abstract class ConfigFile
    {
        public String FilePath { get; set; }

        public ConfigFile() { }
        public ConfigFile(String filePath)
        {
            FilePath = filePath;
            if (!File.Exists(FilePath)) LoadSaveDefault();
            else Load();
        }

        public abstract void LoadSaveDefault(String filePath);
        public void LoadSaveDefault()
        {
            LoadSaveDefault(FilePath);
        }

        public abstract void Load(String filePath);
        public void Load()
        {
            Load(FilePath);
        }

        public abstract void Save(String filePath);
        public void Save()
        {
            Save(FilePath);
        }
    }
}
