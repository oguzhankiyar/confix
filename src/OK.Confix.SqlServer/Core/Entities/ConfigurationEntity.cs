namespace OK.Confix.SqlServer.Core.Entities
{
    public class ConfigurationEntity : BaseEntity
    {
        public int ApplicationId { get; set; }
        
        public int? EnvironmentId { get; set; }
        
        public string Name { get; set; }

        public string Type { get; set; }
        
        public string Value { get; set; }

        
        public virtual ApplicationEntity Application { get; set; }
        
        public virtual EnvironmentEntity Environment { get; set; }
    }
}