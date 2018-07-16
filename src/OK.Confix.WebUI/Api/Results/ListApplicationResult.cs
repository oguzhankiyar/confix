using OK.Confix.Models;
using System.Collections.Generic;

namespace OK.Confix.WebUI.Api.Results
{
    public class ListApplicationResult : BaseResult
    {
        public List<ApplicationModel> ApplicationList { get; set; }
    }
}