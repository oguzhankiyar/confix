using OK.Confix.Models;
using System.Collections.Generic;

namespace OK.Confix.WebUI.Api.Results
{
    public class ListEnvironmentResult
    {
        public bool IsSuccessful { get; set; }

        public List<EnvironmentModel> EnvironmentList { get; set; }
    }
}