using OK.Confix.FileSystem.Models;
using OK.Confix.Helpers;
using System.IO;

namespace OK.Confix.FileSystem.Helpers
{
    internal class FileHelper
    {
        private readonly string _filePath;

        public FileHelper(string filePath)
        {
            _filePath = filePath;

            bool isFileExists = File.Exists(filePath);

            if (!isFileExists)
            {
                StreamWriter sw = File.CreateText(_filePath);
                string json = JsonHelper.Serialize(new FileModel());
                sw.Write(json);
                sw.Flush();
                sw.Close();
            }
        }

        public FileModel GetFile()
        {
            string json = File.ReadAllText(_filePath);

            return JsonHelper.Deserialize<FileModel>(json);
        }

        public FileModel SaveFile(FileModel file)
        {
            string json = JsonHelper.Serialize(file);

            File.Delete(_filePath);

            StreamWriter sw = File.CreateText(_filePath);
            sw.Write(json);
            sw.Flush();
            sw.Close();

            return file;
        }
    }
}