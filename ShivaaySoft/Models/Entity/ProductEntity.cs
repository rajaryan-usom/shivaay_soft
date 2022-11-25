using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShivaaySoft.Models.Entities
{
    [Table("Product")]
    public class ProductEntity : BaseEntity
    {
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
        public string FilePath { get; set; }
        public string Excerpt { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
