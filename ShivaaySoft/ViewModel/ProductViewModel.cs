using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShivaaySoft.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Category is Required")]
        public int CategoryId { get; set; }


        [Required(ErrorMessage = "Short Description is Required")]
        public string Excerpt { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        [MaxLength(200)]
        public string Description { get; set; }
       
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }
    }

}
