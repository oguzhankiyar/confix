using OK.Confix.Models;

namespace OK.Confix.WebUI.Api.Results
{
    public class DetailApplicationResult
    {
        public bool IsSuccessful { get; set; }

        public ApplicationModel Application { get; set; }
    }
}
