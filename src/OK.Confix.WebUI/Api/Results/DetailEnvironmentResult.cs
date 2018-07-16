using OK.Confix.Models;

namespace OK.Confix.WebUI.Api.Results
{
    public class DetailEnvironmentResult : BaseResult
    {
        public EnvironmentModel Environment { get; set; }
    }
}