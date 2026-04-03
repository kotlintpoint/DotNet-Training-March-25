using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> ProductList = _unitOfWork.Products.GetAll().ToList();
            return View(ProductList);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            //ViewBag.CategoryList = CategoryList;
            ProductVM ProductVM = new ProductVM
            {
                CategoryList = CategoryList,
                Product = new Product()
            };
            return View(ProductVM);
        }
        [HttpPost]
        public IActionResult Create(ProductVM ProductVM)
        {            
            if (ProductVM.Product.Title == ProductVM.Product.Description.ToString()) {
                ModelState.AddModelError("title", "The Title cannot exactly match the Description."); 
            }
            ProductVM.Product.ImageUrl = "";
            if (ModelState.IsValid)
            {
                _unitOfWork.Products.Add(ProductVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully.";
                return RedirectToAction("Index");
            }
                        
            ProductVM.CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            return View(ProductVM);
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0) 
            {
                return NotFound();
            }

            //Category? CategoryObj = _categoryRepo.Get(c => c.Id == Id);
            Product? ProductObj = _unitOfWork.Products.Get(c => c.Id == Id);

            if (ProductObj == null) {
                return NotFound();
            }

            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            ViewBag.CategoryList = CategoryList;

            return View(ProductObj);
        }

        [HttpPost]
        public IActionResult Edit(Product Product)
        {           
            if (ModelState.IsValid)
            {
                //_categoryRepo.Update(Category);
                //_categoryRepo.Save();

                _unitOfWork.Products.Update(Product);
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            Product? ProductObj = _unitOfWork.Products.Get(c => c.Id == Id);

            if (ProductObj == null)
            {
                return NotFound();
            }

            return View(ProductObj);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            Product? ProductObj = _unitOfWork.Products.Get(c => c.Id == Id);

            if (ProductObj == null)
            {
                return NotFound();
            }
           
            _unitOfWork.Products.Remove(ProductObj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
