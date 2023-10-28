using first.Models;
using first.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace first.Controllers
{
    public class ProductController : Controller
    {
        IOrderDetailRepository orderDetailServices;

        IProductRepository productServices;
        IOrderRepository orderServices;
        ICategoryRepository categoryServices;
        public ProductController(IProductRepository _productServices, ICategoryRepository _categoryServices,IOrderRepository _orderServices,IOrderDetailRepository _orderDetailServices)
        {
            productServices = _productServices;
            categoryServices = _categoryServices;
            orderServices = _orderServices;
            orderDetailServices = _orderDetailServices;
        }
        //public ProductController(ProductServices _productServices)
        //{
        //    productServices = _productServices;
        //}
        public IActionResult Index()
        {
            return View();
        }
        // GET: product/Details/5
        public IActionResult Details(int id)
        {
            if (productServices.getAll() == null)
            {
                return NotFound();
            }
            var products = productServices.GetById(id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: product/Create
        public IActionResult CreateProduct([FromRoute] int id)
        {
            ViewBag.CategoryId = id;
            return View();
        }

        // POST: product/Create
        [HttpPost]
        public IActionResult AddNewProduct(Product product, [FromForm] IFormFile img_file)
        {

            if (img_file != null)
            {
                product.AdminId = categoryServices.getAdminId(product.CategoryId);
                productServices.Create(product, img_file);
                return RedirectToAction("ShowCategoryProducts", "Category", product.CategoryId);
            }

            return View(product);
        }


        // GET: product/Edit/5
        public IActionResult Edit(int id)
        {
            if (productServices.getAll() == null)
            {
                return NotFound();
            }

            var products = productServices.GetById(id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }
        [HttpPost]
        public IActionResult Edit(int id, Product Myproduct, IFormFile img_file)
        {
            productServices.Update(id, Myproduct, img_file);
            return RedirectToAction(nameof(Index));
        }

        // GET: product/Delete/5
        public IActionResult DeleteCategory(int id)
        {
            if (productServices.getAll() == null)
            {
                return NotFound();
            }

            var products = productServices.GetById(id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: product/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (productServices.getAll() == null)
            {
                return Problem("Entity set 'AppDbContext.products1'  is null.");
            }
            var products = productServices.GetById(id);
            if (products != null)
            {
                productServices.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Show()
        {
             List<Product> list = productServices.getAll();
            
            return View("Index_View", list);
            
        }
        //// ///// //////// //////////////// ///////////////  ///// ///////////// ////////
        /// <summary>
        [HttpGet]
        public IActionResult ShowProduct(int id )
        {
            int UserId = (int)HttpContext.Session.GetInt32("UserId");//
            Order order = orderServices.GetByUserId(UserId);//order 
            OrderDetail orderDetail = orderDetailServices.GetByOrderIdAndProductId(order.Id, id);
            if (orderDetail != null)
            {
                ViewBag.Quantity = orderDetail.Quantity;
            }
            else ViewBag.Quantity = 0;

            Product product = productServices.getById(id);
            return View(product);
        }

    }
}
