using SportsStore.Models;
using System.Linq;

namespace SportsStore.Data
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        IQueryable<Order> GetOrders();
        void SaveOrder(Order order);
    }
}
