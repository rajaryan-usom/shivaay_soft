using ShivaaySoft.Models.Entities;
using System.Collections.Generic;

namespace ShivaaySoft.Models
{
    public class PaginationModel 
        
    {
        public List<ProductEntity> Customers { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    } 
}
