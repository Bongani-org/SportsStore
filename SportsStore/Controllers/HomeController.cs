using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Data;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IRepositoryWrapper _repository;
        public int iPageSize = 4;
        public HomeController(IRepositoryWrapper repo)
        {
            _repository = repo;
        }

        public IActionResult Index(string category = "", int productPage = 1)
        {
            int categoryId;
            if (category == "" | category == "favicon.ico")
            {
                categoryId = 0;
            }
            else
            {
                categoryId = _repository.Category.FindByCondition(p => p.CategoryName == category)
                    .FirstOrDefault().CategoryID;
            }

            return View(new ProductsListViewModel
            {
                Products = _repository.Product.FindByCondition(p => categoryId == 0 ||
                                p.CategoryID == categoryId)
                            .OrderBy(p => p.ProductID)
                            .Skip((productPage - 1) * iPageSize)
                            .Take(iPageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = iPageSize,
                    TotalItems = category == "" ? 
                        _repository.Product.FindAll().Count() :
                        _repository.Product.FindByCondition(p => p.CategoryID == categoryId).Count()
                },
                CurrentCategory = category
            });
        }
    }
}
