using first.Models;
using first.services;
using first.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace first.Controllers
{
    public class CategoryController : Controller
    {
		ICategoryRepository categoryServices;
        IProductRepository productServices;
        IAdminRepository adminServices;
        public CategoryController (ICategoryRepository _categoryServices,IProductRepository _productRepository,IAdminRepository _adminServices)
        {
            categoryServices = _categoryServices;
            productServices = _productRepository;
            adminServices = _adminServices;
		}
       
        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult CreateCategory()
        {
			return View(new Category());
        }

        [HttpPost] 
        public IActionResult AddNewCategory(Category category)//adminId
        {

            if (category != null)
            {
                category.AdminId =(int) HttpContext.Session.GetInt32("AdminId");
                categoryServices.Create(category);

                return RedirectToAction("AdminHome", "Admin");
            }


            return View("CreateCategory",category);
        }
        [HttpGet]
        public IActionResult ShowCategoryProducts([FromRoute] int id)//category id 
        {
            
            ViewBag.CategoryName = categoryServices.getNameById(id);
            ViewBag.CategoryId = id;
			List<Product> Products = productServices.getAll(id);
            return View(Products);
        }
        public IActionResult ShowCategoryProduct([FromRoute] int id)//User 
        {
            
            ViewBag.CategoryName = categoryServices.getNameById(id);
            ViewBag.CategoryId = id;
			List<Product> Products = productServices.getAll(id);
            return View(Products);
        }
        //    Delete Product From a specific Category
        [HttpGet]
		public IActionResult DeleteProduct([FromRoute] int id ,[FromQuery] int CategoryId)
		{

            categoryServices.DeleteProduct(id);
            return RedirectToRoute(new { controller = "Category", action = "ShowCategoryProducts", id = CategoryId });
        }
        public IActionResult DeleteCategory([FromRoute]int id)
        {
            categoryServices.Delete(id);
            return RedirectToRoute(new { controller = "Admin", action = "AdminHome"});

        }

    }
}
