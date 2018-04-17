using Newtonsoft.Json;
using OK.Confix.FileSystem.Models;
using System.IO;

namespace OK.Confix.FileSystem.Helpers
{
    public class FileHelper
    {
        private readonly string _filePath;

        public FileHelper(string filePath)
        {
            _filePath = filePath;

            bool isFileExists = File.Exists(filePath);

            if (!isFileExists)
            {
                StreamWriter sw = File.CreateText(_filePath);
                string json = JsonConvert.SerializeObject(new FileModel(), Formatting.Indented);
                sw.Write(json);
                sw.Flush();
                sw.Close();
            }
        }

        public FileModel GetFile()
        {
            string json = File.ReadAllText(_filePath);

            return JsonConvert.DeserializeObject<FileModel>(json);
        }

        public FileModel SaveFile(FileModel file)
        {
            string json = JsonConvert.SerializeObject(file, Formatting.Indented);

            File.Delete(_filePath);

            StreamWriter sw = File.CreateText(_filePath);
            sw.Write(json);
            sw.Flush();
            sw.Close();

            return file;
        }
    }
}