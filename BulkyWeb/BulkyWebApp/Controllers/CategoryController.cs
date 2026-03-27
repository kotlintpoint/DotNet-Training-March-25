using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> CategoryList = _db.Categories.OrderBy(c => c.DisplayOrder).ToList();
            return View(CategoryList);
        }

        public IActionResult Create()
        {        
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category Category)
        {
            if (Category.Name == Category.DisplayOrder.ToString()) {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name."); 
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(Category);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully.";
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

            Category? CategoryObj = _db.Categories.Find(Id);

            if (CategoryObj == null) {
                return NotFound();
            }

            return View(CategoryObj);
        }

        [HttpPost]
        public IActionResult Edit(Category Category)
        {           
            if (ModelState.IsValid)
            {
                _db.Categories.Update(Category);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully.";
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

            Category? CategoryObj = _db.Categories.Find(Id);

            if (CategoryObj == null)
            {
                return NotFound();
            }

            return View(CategoryObj);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            Category? CategoryObj = _db.Categories.Find(Id);

            if (CategoryObj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(CategoryObj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
