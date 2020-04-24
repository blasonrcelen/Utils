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
            if (!File.Exists(FilePath)) CreateDefaultConfigFile();
            LoadConfigFile();
        }

        public abstract void CreateDefaultConfigFile(String _filePath);
        public void CreateDefaultConfigFile()
        {
            CreateDefaultConfigFile(FilePath);
        }

        public abstract void LoadConfigFile(String _filePath);
        public void LoadConfigFile()
        {
            LoadConfigFile(FilePath);
        }

        public abstract void SaveConfigFile(String _filePath);
        public void SaveConfigFile()
        {
            SaveConfigFile(FilePath);
        }
    }
}
