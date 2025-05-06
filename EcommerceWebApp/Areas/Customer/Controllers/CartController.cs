using EcommerceWebApp.Models;
using EcommerceWebApp.Models.ViewModels;
using EcommerceWebApp.Repository.IRepository;
using EcommerceWebApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceWebApp.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.AppUserId == userId, includeProperties: "Product"),
                OrderHeader = new() 
            };


            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = cart.Product.Price;
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            return View(ShoppingCartVM);
        }
        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.AppUserId == userId,includeProperties: "Product"),
                OrderHeader = new()
            };

            ShoppingCartVM.OrderHeader.AppUser = _unitOfWork.AppUser.Get(u => u.Id == userId);

            ShoppingCartVM.OrderHeader.HouseName = ShoppingCartVM.OrderHeader.AppUser.Name;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.AppUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.AppUser.State;
            ShoppingCartVM.OrderHeader.Address = ShoppingCartVM.OrderHeader.AppUser.StreetAddress;
            ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.AppUser.City;
            ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.AppUser.PostalCode;



            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = cart.Product.Price;
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ActionName("Summary")]
        [Authorize]
        public IActionResult SummaryPOST([FromForm] OrderHeader orderHeader)
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                // Initialize ShoppingCartVM
                ShoppingCartVM = new()
                {
                    ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.AppUserId == userId, includeProperties: "Product"),
                    OrderHeader = orderHeader // Use the posted order header
                };

                // Set user-specific data
                ShoppingCartVM.OrderHeader.AppUserId = userId;
                ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
                ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;

                // Validate required fields
                if (string.IsNullOrEmpty(ShoppingCartVM.OrderHeader.PhoneNumber) ||
                    string.IsNullOrEmpty(ShoppingCartVM.OrderHeader.HouseName) ||
                    string.IsNullOrEmpty(ShoppingCartVM.OrderHeader.Address) ||
                    string.IsNullOrEmpty(ShoppingCartVM.OrderHeader.City) ||
                    string.IsNullOrEmpty(ShoppingCartVM.OrderHeader.PostalCode) ||
                    string.IsNullOrEmpty(ShoppingCartVM.OrderHeader.State))
                {
                    TempData["Error"] = "Please fill in all required fields.";
                    return RedirectToAction(nameof(Summary));
                }

                // Calculate order total
                ShoppingCartVM.OrderHeader.OrderTotal = 0;
                foreach (var cart in ShoppingCartVM.ShoppingCartList)
                {
                    cart.Price = cart.Product.Price;
                    ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
                }

                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        // Save order header
                        _unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
                        _unitOfWork.Save();

                        // Save order details
                        foreach (var cart in ShoppingCartVM.ShoppingCartList)
                        {
                            OrderDetail orderDetail = new()
                            {
                                ProductId = cart.ProductId,
                                OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
                                Price = cart.Price,
                                Count = cart.Count
                            };
                            _unitOfWork.OrderDetail.Add(orderDetail);
                        }
                        _unitOfWork.Save();

                        // Clear shopping cart
                        _unitOfWork.ShoppingCart.RemoveRange(ShoppingCartVM.ShoppingCartList);
                        _unitOfWork.Save();

                        transaction.Commit();

                        // Reset session cart count
                        HttpContext.Session.SetInt32(SD.SessionCart, 0);

                        return RedirectToAction(nameof(OrderConfirmation), new { id = ShoppingCartVM.OrderHeader.Id });
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        TempData["Error"] = "An error occurred while processing your order. Please try again.";
                        return RedirectToAction(nameof(Summary));
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while processing your order. Please try again.";
                return RedirectToAction(nameof(Summary));
            }
        }


        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == id, includeProperties: "AppUser");
            _unitOfWork.OrderHeader.UpdateStatus(orderHeader.Id, SD.StatusInProcess);
            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetAll(u => u.AppUserId == orderHeader.AppUserId).ToList();       
            _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
            _unitOfWork.Save();
            return View(id);
        }


        public IActionResult Plus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId, tracked: true);
            cartFromDb.Count += 1;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId, tracked: true);
            if (cartFromDb.Count <= 1)
            {
                //remove that from cart

                _unitOfWork.ShoppingCart.Remove(cartFromDb);
                HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.AppUserId == cartFromDb.AppUserId).Count() - 1);
            }
            else
            {
                cartFromDb.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);


            _unitOfWork.ShoppingCart.Remove(cartFromDb);
            _unitOfWork.Save();
            HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.AppUserId == cartFromDb.AppUserId).Count() - 1);
            return RedirectToAction(nameof(Index));
        }
    }
}
