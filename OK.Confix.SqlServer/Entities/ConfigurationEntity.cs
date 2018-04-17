using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OK.Confix.SqlServer.Entities
{
    [Table("Configurations")]
    public class ConfigurationEntity : BaseEntity
    {
        [Required]
        public int ApplicationId { get; set; }
        
        public int? EnvironmentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }


        [ForeignKey("ApplicationId")]
        public virtual ApplicationEntity Application { get; set; }

        [ForeignKey("EnvironmentId")]
        public virtual EnvironmentEntity Environment { get; set; }
    }
}