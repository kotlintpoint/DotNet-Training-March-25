using Bulky.DataAccess.Repository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyWebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCartVM = new ShoppingCartVM
            {
                ShoppingCartList = _unitOfWork.ShoppingCarts.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product")
            };
            foreach (var ShoppingCart in ShoppingCartVM.ShoppingCartList) {                
                ShoppingCart.Price = GetPriceBasedOnQuantity(ShoppingCart);
                ShoppingCartVM.OrderTotal += ShoppingCart.Price * ShoppingCart.Count;
            }

            return View(ShoppingCartVM);
        }

        private double GetPriceBasedOnQuantity(ShoppingCart ShoppingCart) {
            if (ShoppingCart.Count <= 50)
            {
                return ShoppingCart.Product.Price;
            }
            else {
                if (ShoppingCart.Count <= 100) {
                    return ShoppingCart.Product.Price50;
                }
                return ShoppingCart.Product.Price100;
            }
        }

        public IActionResult Plus(int CartId) {
            var ShoppingCart = _unitOfWork.ShoppingCarts.Get(u => u.Id == CartId);
            ShoppingCart.Count += 1;
            _unitOfWork.ShoppingCarts.Update(ShoppingCart);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int CartId)
        {
            var ShoppingCart = _unitOfWork.ShoppingCarts.Get(u => u.Id == CartId);
            if (ShoppingCart.Count <= 1)
            {
                _unitOfWork.ShoppingCarts.Remove(ShoppingCart);
            }
            else {
                ShoppingCart.Count -= 1;
                _unitOfWork.ShoppingCarts.Update(ShoppingCart);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int CartId)
        {
            var ShoppingCart = _unitOfWork.ShoppingCarts.Get(u => u.Id == CartId);
            _unitOfWork.ShoppingCarts.Remove(ShoppingCart);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary() {
            return View();
        }
    }
}
