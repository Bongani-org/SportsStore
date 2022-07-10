namespace SportsStore.Data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private AppDbContext _appDbContext;
        private IProductRepository _product;
        private ICategoryRepository _category;
        private IOrderRepository _order;

        public RepositoryWrapper(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IProductRepository Product
        {
            get
            {
                if (_product == null)
                {
                    _product = new EFProductRepository(_appDbContext);
                }

                return _product;
            }
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new EFCategoryRepository(_appDbContext);
                }

                return _category;
            }
        }

        public IOrderRepository Order
        {
            get
            {
                if (_order == null)
                {
                    _order = new EFOrderRepository(_appDbContext);
                }

                return _order;
            }
        }
    }
}
