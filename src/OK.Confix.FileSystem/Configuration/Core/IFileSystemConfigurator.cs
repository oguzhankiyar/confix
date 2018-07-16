namespace OK.Confix.FileSystem.Configuration.Core
{
    public interface IFileSystemConfigurator
    {
        IFileSystemConfigurator SetPath(string path);

        IFileSystemConfigurator SetFileName(string name);
    }
}