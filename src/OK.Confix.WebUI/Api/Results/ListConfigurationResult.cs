using OK.Confix.Models;
using System.Collections.Generic;

namespace OK.Confix.WebUI.Api.Results
{
    public class ListConfigurationResult : BaseResult
    {
        public List<ConfigurationModel> ConfigurationList { get; set; }
    }
}