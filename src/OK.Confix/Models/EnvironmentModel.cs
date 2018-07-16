namespace OK.Confix.Models
{
    public class EnvironmentModel
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }

        public ApplicationModel Application { get; set; }

        public string Name { get; set; }
    }
}