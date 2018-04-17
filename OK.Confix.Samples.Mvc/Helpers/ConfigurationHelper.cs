namespace OK.Confix.Samples.Mvc.Helpers
{
    public class ConfigurationHelper
    {
        public static ConfixContext Confix;

        public string Get(string key)
        {
            if (Confix == null)
                return null;

            return Confix.Get<string>(key);
        }

        public void Set(string key, string value)
        {
            if (Confix != null)
                Confix.Set(key, value);
        }
    }
}