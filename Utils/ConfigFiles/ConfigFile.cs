using System;
using System.IO;

namespace Utils.ConfigFiles
{
    public abstract class ConfigFile
    {
        public String FilePath { get; set; }

        public ConfigFile() { }
        public ConfigFile(String _filePath)
        {
            FilePath = _filePath;
            if (!File.Exists(FilePath)) LoadSaveDefault();
            else Load();
        }

        public abstract void LoadSaveDefault(String _filePath);
        public void LoadSaveDefault()
        {
            LoadSaveDefault(FilePath);
        }

        public abstract void Load(String _filePath);
        public void Load()
        {
            Load(FilePath);
        }

        public abstract void Save(String _filePath);
        public void Save()
        {
            Save(FilePath);
        }
    }
}
