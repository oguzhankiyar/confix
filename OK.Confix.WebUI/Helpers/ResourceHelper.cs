using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace OK.Confix.WebUI.Helpers
{
    public static class ResourceHelper
    {
        private static Dictionary<string, string> cachedFiles = new Dictionary<string, string>();

        public static string Read(string fileName)
        {
            if (cachedFiles.ContainsKey(fileName))
            {
                return cachedFiles[fileName];
            }

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();

                cachedFiles.Add(fileName, result);

                return result;
            }
        }
    }
}