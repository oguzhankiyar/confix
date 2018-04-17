namespace OK.Confix.FileSystem.Entities
{
    public class ConfigurationEntity : BaseEntity
    {
        public int ApplicationId { get; set; }
        
        public int? EnvironmentId { get; set; }
        
        public string Name { get; set; }
        
        public string Value { get; set; }
    }
}