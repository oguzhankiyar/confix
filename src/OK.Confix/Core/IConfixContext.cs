namespace OK.Confix
{
    public interface IConfixContext
    {
        T Get<T>(string key);

        void Set<T>(string key, T value);
    }
}