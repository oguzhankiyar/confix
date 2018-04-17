namespace OK.Confix.Models
{
    public class ConfigurationModel
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }

        public ApplicationModel Application { get; set; }

        public int? EnvironmentId { get; set; }

        public EnvironmentModel Environment { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}