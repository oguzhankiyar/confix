namespace OK.Confix.WebUI.Api.Results
{
    public abstract class BaseResult
    {
        public bool IsSuccessful { get; set; }

        public string Message { get; set; }
    }
}