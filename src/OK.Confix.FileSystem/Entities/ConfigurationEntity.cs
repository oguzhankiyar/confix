namespace OK.Confix.FileSystem.Entities
{
    internal class ConfigurationEntity : BaseEntity
    {
        public int ApplicationId { get; set; }
        
        public int? EnvironmentId { get; set; }
        
        public string Name { get; set; }
        
        public string Type { get; set; }

        public string Value { get; set; }
    }
}