using OK.Confix.FileSystem.Configuration.Core;

namespace OK.Confix.FileSystem.Configuration
{
    public class FileSystemConfigurator : IFileSystemConfigurator
    {
        private FileSystemDataProvider _dataProvider;

        public FileSystemConfigurator(FileSystemDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public IFileSystemConfigurator SetPath(string path)
        {
            _dataProvider.Settings.Path = path;

            return this;
        }

        public IFileSystemConfigurator SetFileName(string fileName)
        {
            _dataProvider.Settings.FileName = fileName;

            return this;
        }
    }
}