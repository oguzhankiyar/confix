using OK.Confix.Models;

namespace OK.Confix.WebUI.Api.Results
{
    public class DetailConfigurationResult : BaseResult
    {
        public ConfigurationModel Configuration { get; set; }
    }
}