using System.ComponentModel.DataAnnotations;

namespace ShivaaySoft.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is Required")]
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }
}
