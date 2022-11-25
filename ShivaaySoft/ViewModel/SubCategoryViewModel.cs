using System.ComponentModel.DataAnnotations;

namespace ShivaaySoft.ViewModels
{
    public class SubCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Category is Required")]
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
