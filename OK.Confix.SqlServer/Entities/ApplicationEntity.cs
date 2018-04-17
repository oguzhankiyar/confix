using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OK.Confix.SqlServer.Entities
{
    [Table("Applications")]
    public class ApplicationEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}