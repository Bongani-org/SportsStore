using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Data;
using SportsStore.Infrastructure;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Pages
{
    public class CartModel : PageModel
    {
        private readonly IRepositoryWrapper _repository;
        public CartModel(IRepositoryWrapper repository, Cart cartService)
        {
            _repository = repository;
            Cart = cartService;
        }

        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            //Set value of return url
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(int productID, string returnUrl)
        {
            //Retrieve a Product from the database
            Product product = _repository.Product.GetById(productID);

            //Update Cart content using the Product
            Cart.AddItem(product, 1);

            //Redirect browser to the same Razor Page - using a GET request
            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(int productID, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(cl =>
                cl.Product.ProductID == productID).Product);
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}