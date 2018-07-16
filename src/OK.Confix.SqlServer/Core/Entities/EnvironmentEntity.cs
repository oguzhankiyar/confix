namespace OK.Confix.SqlServer.Core.Entities
{
    public class EnvironmentEntity : BaseEntity
    {
        public int ApplicationId { get; set; }
        
        public string Name { get; set; }

        
        public virtual ApplicationEntity Application { get; set; }
    }
}