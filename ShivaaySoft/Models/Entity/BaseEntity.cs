using System;

namespace ShivaaySoft.Models.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDateTime { get; set; } = DateTime.UtcNow.ToLocalTime();
    }
}
