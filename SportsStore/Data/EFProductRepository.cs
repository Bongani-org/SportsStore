using SportsStore.Models;

namespace SportsStore.Data
{
    public class EFProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public EFProductRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
        }

    }
}
