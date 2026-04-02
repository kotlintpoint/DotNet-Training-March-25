using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product Product)
        {
            if (Product.Title == Product.Description.ToString()) {
                ModelState.AddModelError("title", "The Title cannot exactly match the Description."); 
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Products.Add(Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully.";
                return RedirectToAction("Index");
            }
            return View();
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
