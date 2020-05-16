using System;
using System.IO;

namespace Utils.ConfigFiles
{
    public abstract class ConfigFile
    {
        public string FilePath { get; set; }

        public ConfigFile() { }
        public ConfigFile(string filePath)
        {
            FilePath = filePath;
            if (!File.Exists(FilePath)) LoadSaveDefault();
            else Load();
        }

        public abstract void LoadSaveDefault(string filePath);
        public void LoadSaveDefault()
        {
            LoadSaveDefault(FilePath);
        }

        public abstract void Load(string filePath);
        public void Load()
        {
            Load(FilePath);
        }

        public abstract void Save(string filePath);
        public void Save()
        {
            Save(FilePath);
        }
    }
}
