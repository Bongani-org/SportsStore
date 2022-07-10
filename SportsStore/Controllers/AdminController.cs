using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsStore.Data;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class AdminController : Controller
    {
        private IRepositoryWrapper _repository;

        public AdminController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View(_repository.Product.FindAll());
        }

        public IActionResult Edit(int productId)
        {
            var product = _repository.Product.GetById(productId);

            if (product == null)
                return NotFound();

            ViewBag.Title = "Edit Product";
            PopulateCategoryDDL(product.CategoryID);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Product.Update(product);
                    _repository.Product.Save();
                    TempData["Message"] = $"{product.Name} has been saved";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(product);
        }
        public IActionResult Create()
        {
            PopulateCategoryDDL();
            ViewBag.Title = "Add Product";
            return View("Edit", new Product());
        }

        private void PopulateCategoryDDL(object selectedCategory = null)
        {
            ViewBag.CategoryID = new SelectList(_repository.Category.FindAll(),
                "CategoryID", "CategoryName", selectedCategory);
        }

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            var product = _repository.Product.GetById(productId);
            if(product != null)
            {
                try
                {
                    _repository.Product.Delete(product);
                    _repository.Product.Save();
                    TempData["Message"] = $"{product.Name} was deleted";
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to delete product. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Product does not exist.");
            }
            return RedirectToAction("Index");
        }
    }
}
