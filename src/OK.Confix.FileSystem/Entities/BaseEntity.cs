using System;

namespace OK.Confix.FileSystem.Entities
{
    internal abstract class BaseEntity
    {
        public int Id { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime UpdatedDate { get; set; }
    }
}