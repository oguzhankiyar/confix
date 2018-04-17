namespace OK.Confix.WebUI.Api.Inputs
{
    public class CreateConfigurationInput
    {
        public int ApplicationId { get; set; }

        public int? EnvironmentId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}