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
            if (!File.Exists(FilePath)) SaveDefaultConfigFile();
            LoadConfigFile();
        }

        public abstract void SaveDefaultConfigFile(String _filePath);
        public void SaveDefaultConfigFile()
        {
            SaveDefaultConfigFile(FilePath);
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
