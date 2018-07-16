namespace OK.Confix.WebUI.Api.Inputs
{
    public class EditConfigurationInput
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }

        public int? EnvironmentId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }
    }
}