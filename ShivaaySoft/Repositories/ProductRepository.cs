using ShivaaySoft.Contract;
using ShivaaySoft.Data;
using ShivaaySoft.Models.Entities;

namespace ShivaaySoft.Repositories
{
    public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
