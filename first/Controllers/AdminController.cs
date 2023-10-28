using first.Models;
using first.services;
using first.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace first.Controllers
{
    public class AdminController : Controller
    {
        IAdminRepository adminServices;
        ICategoryRepository categoryServices;
        IProductRepository productServices;
        public AdminController(IAdminRepository _adminServices, ICategoryRepository _categoryServices, IProductRepository _productServices)
        {
            adminServices = _adminServices;
            categoryServices = _categoryServices;
            productServices = _productServices;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ConfirmAdmin(string Email, string Password)
        {


            Admin admin = adminServices.GetAdminByEmail(Email);
            if (admin != null && admin.Password == Password)
            {
                HttpContext.Session.SetInt32("AdminId", admin.Id);
                return RedirectToAction("AdminHome");
            }

            return View("Login");
        }
        public IActionResult AdminHome()
        {
            
           // List<Category> Categories = categoryServices.getAll();
            //ViewBag.Products = productServices.getAll();
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        //////////////////////////////// Product --?
        public IActionResult CreateProduct()//categoryId  
        {
           var Categorys = categoryServices.getAll();
        
            
            ViewBag.Names = Categorys  ;
            return View(new ProductCategory());
        }

        // POST: product/Create
        [HttpPost]
        public IActionResult AddNewProduct(ProductCategory productC, [FromForm] IFormFile img_file)
        {

            Product product = new Product();

            if (img_file != null)
            {
                product.Name = productC.Name;
                product.Description = productC.Description;
                product.Price = productC.Price;
                product.StockQuantity = productC.StockQuantity;
                product.CategoryId = categoryServices.getIdByName(productC.CategoryName);

              //  int id = product.Id;
                product.AdminId = (int)HttpContext.Session.GetInt32("AdminId");
                productServices.Create(product, img_file);
                // return RedirectToAction("ShowCategoryProducts", "Category", new {id});
                return RedirectToRoute(new { controller = "Category", action = "ShowCategoryProducts" , id= product.CategoryId });

            }

            List<Category> Categories = categoryServices.getAll();
            ViewBag.Products = productServices.getAll();
            return RedirectToRoute(new { controller = "Admin", action = "CreateProduct",productC });

        
        }
        public IActionResult EditProduct(int id)
        {
            Product product = productServices.GetById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult SaveEditedProduct(Product product, [FromForm] IFormFile img_file)
        {
            if (img_file != null)
            {
                //  int id = product.Id;
                product.AdminId = (int)HttpContext.Session.GetInt32("AdminId");
                productServices.Update(product.Id, product, img_file);

                // return RedirectToAction("ShowCategoryProducts", "Category", new {id});
             
            }
            return RedirectToRoute(new { controller = "Category", action = "ShowCategoryProducts", id = product.CategoryId });
        }

    } 
}
