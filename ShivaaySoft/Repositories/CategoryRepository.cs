using ShivaaySoft.Contract;
using ShivaaySoft.Data;
using ShivaaySoft.Models.Entities;

namespace ShivaaySoft.Repositories
{
    public class CategoryRepository : BaseRepository<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
