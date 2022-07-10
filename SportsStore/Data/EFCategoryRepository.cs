using SportsStore.Models;

namespace SportsStore.Data
{
    public class EFCategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public EFCategoryRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
        }

    }
}
