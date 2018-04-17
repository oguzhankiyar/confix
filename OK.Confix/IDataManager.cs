namespace OK.Confix
{
    public interface IDataManager
    {
        T Get<T>(string key);

        void Set<T>(string key, T value);
    }
}