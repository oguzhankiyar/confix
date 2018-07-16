using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OK.Confix.Helpers
{
    public static class JsonHelper
    {
        private static readonly JsonSerializerSettings camelCaseSerializerSettings =
            new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

        public static string Serialize<T>(T obj, bool isCamelCase = true)
        {
            if (isCamelCase)
            {
                return JsonConvert.SerializeObject(obj, camelCaseSerializerSettings);
            }

            return JsonConvert.SerializeObject(obj);
        }

        public static T Deserialize<T>(string json, bool isCamelCase = true)
        {
            if (isCamelCase)
            {
                return JsonConvert.DeserializeObject<T>(json, camelCaseSerializerSettings);
            }

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}