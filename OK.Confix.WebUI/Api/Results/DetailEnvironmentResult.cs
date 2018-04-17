using OK.Confix.Models;

namespace OK.Confix.WebUI.Api.Results
{
    public class DetailEnvironmentResult
    {
        public bool IsSuccessful { get; set; }

        public EnvironmentModel Environment { get; set; }
    }
}