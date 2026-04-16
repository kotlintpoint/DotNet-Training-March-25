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
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
     
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {         
            List<Company> CompanyList = _unitOfWork.Companies.GetAll().ToList();
            return View(CompanyList);
        }

        public IActionResult Upsert(int? Id) 
        {
            Company CompanyObj;
            if (Id == null || Id == 0)
            {
                CompanyObj = new Company();
            }
            else {
                CompanyObj = _unitOfWork.Companies.Get(c => c.Id == Id);
            }
            return View(CompanyObj);
        }

        [HttpPost]
        public IActionResult Upsert(Company Company)
        {
            if (ModelState.IsValid)
            {
                string msg = "";
                if (Company.Id == 0)
                {
                    // Create
                    _unitOfWork.Companies.Add(Company);                    
                    msg = "Company created successfully.";
                }
                else {
                    // Update
                    _unitOfWork.Companies.Update(Company);                    
                    msg = "Company updated successfully.";
                }
                _unitOfWork.Save();
                TempData["success"] = msg;
                return RedirectToAction("Index");
            }
            return View(Company);
        }       

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
                        
            Company? CompanyObj = _unitOfWork.Companies.Get(c => c.Id == Id);

            if (CompanyObj == null)
            {
                return NotFound();
            }

            return View(CompanyObj);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {            
            Company? CompanyObj = _unitOfWork.Companies.Get(c => c.Id == Id);

            if (CompanyObj == null)
            {
                return NotFound();
            }
            _unitOfWork.Companies.Remove(CompanyObj);
            _unitOfWork.Save();
            TempData["success"] = "Company deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
