using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Data;
using SportsStore.Models;

namespace SportsStore.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly IRepositoryWrapper _repository;
        public OrdersModel(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public IEnumerable<Order> UnshippedOrders { get; set; }
        public IEnumerable<Order> ShippedOrders { get; set; }

        public void RetrieveOrders()
        {
            UnshippedOrders = _repository.Order.GetOrders().Where(o => !o.Shipped);
            ShippedOrders = _repository.Order.GetOrders().Where(o => o.Shipped);
        }
        public void OnGet()
        {
            RetrieveOrders();
        }

        public IActionResult OnPost(int Orderid)
        {
            var order = _repository.Order.GetById(Orderid);
            if (order != null)
            {
                order.Shipped = true;
                _repository.Order.Save();
            }

            return RedirectToPage();
        }
    }
}
