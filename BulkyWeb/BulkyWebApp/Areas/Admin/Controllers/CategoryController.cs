using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        //private readonly ICategoryRepository _categoryRepo;
        private readonly IUnitOfWork _unitOfWork;

        //public CategoryController(ICategoryRepository categoryRepo)
        //{
        //    _categoryRepo = categoryRepo;
        //}

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //List<Category> CategoryList = _categoryRepo.GetAll().ToList();
            List<Category> CategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(CategoryList);
        }

        public IActionResult Upsert(int? Id) 
        {
            Category CategoryObj;
            if (Id == null || Id == 0)
            {
                CategoryObj = new Category();
            }
            else {
                CategoryObj = _unitOfWork.Category.Get(c => c.Id == Id);
            }
            return View(CategoryObj);
        }

        [HttpPost]
        public IActionResult Upsert(Category Category)
        {
            if (ModelState.IsValid)
            {
                string msg = "";
                if (Category.Id == 0)
                {
                    // Create
                    _unitOfWork.Category.Add(Category);                    
                    msg = "Category created successfully.";
                }
                else {
                    // Update
                    _unitOfWork.Category.Update(Category);                    
                    msg = "Category updated successfully.";
                }
                _unitOfWork.Save();
                TempData["success"] = msg;
                return RedirectToAction("Index");
            }
            return View(Category);
        }
        //public IActionResult Create()
        //{        
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Create(Category Category)
        //{
        //    if (Category.Name == Category.DisplayOrder.ToString()) {
        //        ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name."); 
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        //_categoryRepo.Add(Category);
        //        //_categoryRepo.Save();
        //        _unitOfWork.Category.Add(Category);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Category created successfully.";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //public IActionResult Edit(int? Id)
        //{
        //    if (Id == null || Id == 0) 
        //    {
        //        return NotFound();
        //    }

        //    //Category? CategoryObj = _categoryRepo.Get(c => c.Id == Id);
        //    Category? CategoryObj = _unitOfWork.Category.Get(c => c.Id == Id);

        //    if (CategoryObj == null) {
        //        return NotFound();
        //    }

        //    return View(CategoryObj);
        //}

        //[HttpPost]
        //public IActionResult Edit(Category Category)
        //{           
        //    if (ModelState.IsValid)
        //    {
        //        //_categoryRepo.Update(Category);
        //        //_categoryRepo.Save();

        //        _unitOfWork.Category.Update(Category);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Category updated successfully.";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            //Category? CategoryObj = _categoryRepo.Get(c => c.Id == Id);
            Category? CategoryObj = _unitOfWork.Category.Get(c => c.Id == Id);

            if (CategoryObj == null)
            {
                return NotFound();
            }

            return View(CategoryObj);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            //Category? CategoryObj = _categoryRepo.Get(c => c.Id == Id);
            Category? CategoryObj = _unitOfWork.Category.Get(c => c.Id == Id);

            if (CategoryObj == null)
            {
                return NotFound();
            }
           //_categoryRepo.Remove(CategoryObj);
           // _categoryRepo.Save();
            _unitOfWork.Category.Remove(CategoryObj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
