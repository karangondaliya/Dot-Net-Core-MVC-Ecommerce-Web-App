using EcommerceWebApp.Models;
using EcommerceWebApp.Repository;
using EcommerceWebApp.Repository.IRepository;
using EcommerceWebApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace EcommerceWebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _uow;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _uow.Product.GetAll(includeProperties: "Category");
            return View(productList);
        }

        public IActionResult Details(int id)
        {
            ShoppingCart cart = new()
            {
                Product = _uow.Product.Get(u => u.Id == id, includeProperties: "Category"),
                Count = 1,
                ProductId = id
            };
            return View(cart);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.AppUserId = userId;

            ShoppingCart cartFromDb = _uow.ShoppingCart.Get(u => u.AppUserId == userId &&
            u.ProductId == shoppingCart.ProductId);
            shoppingCart.Id = 0;
            if (cartFromDb != null)
            {
                //shopping cart exists
                cartFromDb.Count += shoppingCart.Count;
                _uow.ShoppingCart.Update(cartFromDb);
                _uow.Save();
            }
            else
            {
                //add cart record
                _uow.ShoppingCart.Add(shoppingCart);
                _uow.Save();
                HttpContext.Session.SetInt32(SD.SessionCart, _uow.ShoppingCart.GetAll(u => u.AppUserId == userId).ToList().Count);
            }
            TempData["success"] = "Cart updated successfully";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
