using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            //List<Product> ProductList = _unitOfWork.Products.GetAll(includeProperties: "Category").ToList();
            //return View(ProductList);
            return View();
        }

        public IActionResult Upsert(int? Id)
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            ProductVM ProductVM = new ProductVM
            {
                CategoryList = CategoryList
            };

            if (Id == null || Id == 0)
            {
                // Create               
                ProductVM.Product = new Product();
            }
            else {
                // Edit
                Product? ProductObj = _unitOfWork.Products.Get(c => c.Id == Id);

                if (ProductObj == null)
                {
                    return NotFound();
                }
                ProductVM.Product = ProductObj;
            }
            return View(ProductVM);
        }


        [HttpPost]
        public IActionResult Upsert(ProductVM ProductVM, IFormFile? file)
        {
            if (ProductVM.Product.Title == ProductVM.Product.Description.ToString())
            {
                ModelState.AddModelError("title", "The Title cannot exactly match the Description.");
            }

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(ProductVM.Product.ImageUrl))
                    {
                        string oldImagePath = Path.Combine(wwwRootPath, ProductVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    ProductVM.Product.ImageUrl = @"\images\product\" + fileName;
                }
                else {
                    ProductVM.Product.ImageUrl = "";
                }

                string msg;
                if (ProductVM.Product.Id == 0)
                {
                    _unitOfWork.Products.Add(ProductVM.Product);
                    msg = "Product created successfully.";
                }
                else {
                    _unitOfWork.Products.Update(ProductVM.Product);
                    msg = "Product updated successfully.";
                }
                _unitOfWork.Save();
                TempData["success"] = msg;
                return RedirectToAction("Index");
            }

            ProductVM.CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            return View(ProductVM);
        }

        //[HttpPost]
        //public IActionResult Create(ProductVM ProductVM)
        //{
        //    if (ProductVM.Product.Title == ProductVM.Product.Description.ToString())
        //    {
        //        ModelState.AddModelError("title", "The Title cannot exactly match the Description.");
        //    }
        //    ProductVM.Product.ImageUrl = "";
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Products.Add(ProductVM.Product);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Product created successfully.";
        //        return RedirectToAction("Index");
        //    }

        //    ProductVM.CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
        //    {
        //        Text = c.Name,
        //        Value = c.Id.ToString()
        //    });
        //    return View(ProductVM);
        //}

        //public IActionResult Create()
        //{
        //    IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
        //    {
        //        Text = c.Name,
        //        Value = c.Id.ToString()
        //    });
        //    //ViewBag.CategoryList = CategoryList;
        //    ProductVM ProductVM = new ProductVM
        //    {
        //        CategoryList = CategoryList,
        //        Product = new Product()
        //    };
        //    return View(ProductVM);
        //}
        //[HttpPost]
        //public IActionResult Create(ProductVM ProductVM)
        //{            
        //    if (ProductVM.Product.Title == ProductVM.Product.Description.ToString()) {
        //        ModelState.AddModelError("title", "The Title cannot exactly match the Description."); 
        //    }
        //    ProductVM.Product.ImageUrl = "";
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Products.Add(ProductVM.Product);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Product created successfully.";
        //        return RedirectToAction("Index");
        //    }

        //    ProductVM.CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
        //    {
        //        Text = c.Name,
        //        Value = c.Id.ToString()
        //    });
        //    return View(ProductVM);
        //}

        //public IActionResult Edit(int? Id)
        //{
        //    if (Id == null || Id == 0) 
        //    {
        //        return NotFound();
        //    }

        //    //Category? CategoryObj = _categoryRepo.Get(c => c.Id == Id);
        //    Product? ProductObj = _unitOfWork.Products.Get(c => c.Id == Id);

        //    if (ProductObj == null) {
        //        return NotFound();
        //    }

        //    IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
        //    {
        //        Text = c.Name,
        //        Value = c.Id.ToString()
        //    });
        //    ViewBag.CategoryList = CategoryList;

        //    return View(ProductObj);
        //}

        //[HttpPost]
        //public IActionResult Edit(Product Product)
        //{           
        //    if (ModelState.IsValid)
        //    {
        //        //_categoryRepo.Update(Category);
        //        //_categoryRepo.Save();

        //        _unitOfWork.Products.Update(Product);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Product updated successfully.";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //public IActionResult Delete(int? Id)
        //{
        //    if (Id == null || Id == 0)
        //    {
        //        return NotFound();
        //    }

        //    Product? ProductObj = _unitOfWork.Products.Get(c => c.Id == Id);

        //    if (ProductObj == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(ProductObj);
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int? Id)
        //{
        //    Product? ProductObj = _unitOfWork.Products.Get(c => c.Id == Id);

        //    if (ProductObj == null)
        //    {
        //        return NotFound();
        //    }

        //    _unitOfWork.Products.Remove(ProductObj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Product deleted successfully.";
        //    return RedirectToAction("Index");
        //}


        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> ProductList = _unitOfWork.Products.GetAll(includeProperties: "Category").ToList();
            return Json(new {  data = ProductList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Product? ProductObj = _unitOfWork.Products.Get(c => c.Id == id);

            if (ProductObj == null)
            {
                return Json(new { success = false, message = "Error while deleting." }); 
            }

            _unitOfWork.Products.Remove(ProductObj);
            _unitOfWork.Save();
            //TempData["success"] = "Product deleted successfully.";
            return Json(new { success= true, message = "Product deleted successfully." });
        }

        #endregion
    }
}