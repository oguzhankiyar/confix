using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OK.Confix.SqlServer.Entities
{
    [Table("Environments")]
    public class EnvironmentEntity : BaseEntity
    {
        [Required]
        public int ApplicationId { get; set; }

        [Required]
        public string Name { get; set; }


        [ForeignKey("ApplicationId")]
        public virtual ApplicationEntity Application { get; set; }
    }
}