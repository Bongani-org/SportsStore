using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Data;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IRepositoryWrapper _repository;
        private readonly Cart _cart;

        public OrderController(IRepositoryWrapper repository, Cart cartService)
        {
            _repository = repository;
            _cart = cartService;

        }
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray();
                _repository.Order.SaveOrder(order);
                var orderId = order.OrderID;
                _cart.Clear();
                return RedirectToPage("/Completed", new { orderId });
            }
            else
            {
                return View();
            }
        }
    }
}
