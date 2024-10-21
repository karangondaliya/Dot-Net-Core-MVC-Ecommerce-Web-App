using EcommerceWebApp.Data;
using EcommerceWebApp.Models;
using EcommerceWebApp.Models.ViewModels;
using EcommerceWebApp.Repository.IRepository;
using EcommerceWebApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork uow, IWebHostEnvironment webHostEnvironment)
        {
            _uow = uow;
			_webHostEnvironment = webHostEnvironment;

		}
        public IActionResult Index()
        {
            List<Product> objProductList = _uow.Product.GetAll(includeProperties:"Category").ToList();

            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
			{
				Product = new Product(),
				CategoryList = _uow.Category.GetAll(includeProperties:null).Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.Id.ToString()
				})
			};
            if(id == 0 || id == null)
            {
                //Create
				return View(productVM);
			}
            else
            {
                //Update
                productVM.Product = _uow.Product.Get(u => u.Id == id, includeProperties:null);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
                    productVM.Product.ImageUrl = @"\images\" + fileName;
                }

                if (productVM.Product.Id == 0)
				{
					_uow.Product.Add(productVM.Product);
                    TempData["success"] = "Product Created Successfully";
                }
				else
				{
					_uow.Product.Update(productVM.Product);
                    TempData["success"] = "Product Updated Successfully";
                }

                _uow.Save();
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _uow.Category.GetAll(includeProperties:null).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
				return View(productVM);
			}
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product productFromDb = _uow.Product.Get(u => u.Id == id, includeProperties: null);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _uow.Product.Update(obj);
                _uow.Save();
                TempData["success"] = "Product Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product productFromDb = _uow.Product.Get(u => u.Id == id, includeProperties: null);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product obj = _uow.Product.Get(u => u.Id == id, includeProperties: null);
            if (obj == null)
            {
                return NotFound();
            }
            _uow.Product.Remove(obj);
            _uow.Save();
            TempData["success"] = "Product Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
