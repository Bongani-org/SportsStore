using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Data
{
    public class EFOrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public EFOrderRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
        }

        public IQueryable<Order> GetOrders()
        {
            return _appDbContext.Orders
                .Include(o => o.Lines)
                .ThenInclude(l => l.Product);
        }

        public void SaveOrder(Order order)
        {
            _appDbContext.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                _appDbContext.Orders.Add(order);
            }
            _appDbContext.SaveChanges();
        }
    }
}
