using OK.Confix.Models;
using System.Collections.Generic;

namespace OK.Confix.WebUI.Api.Results
{
    public class ListEnvironmentResult : BaseResult
    {
        public List<EnvironmentModel> EnvironmentList { get; set; }
    }
}