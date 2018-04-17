using OK.Confix.Models;
using System.Collections.Generic;

namespace OK.Confix.WebUI.Api.Results
{
    public class ListApplicationResult
    {
        public bool IsSuccessful { get; set; }

        public List<ApplicationModel> ApplicationList { get; set; }
    }
}