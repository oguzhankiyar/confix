using OK.Confix.Models;
using System.Collections.Generic;

namespace OK.Confix.WebUI.Api.Results
{
    public class ListConfigurationResult
    {
        public bool IsSuccessful { get; set; }

        public List<ConfigurationModel> ConfigurationList { get; set; }
    }
}