using OK.Confix.Models;

namespace OK.Confix.WebUI.Api.Results
{
    public class DetailConfigurationResult
    {
        public bool IsSuccessful { get; set; }

        public ConfigurationModel Configuration { get; set; }
    }
}