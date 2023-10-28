using Microsoft.AspNetCore.Mvc;
using first.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using first.services;
using System;
using System.Collections.Generic;

namespace first.Controllers
{
    public class UserController : Controller
    {
        IOrderRepository orderServices;
        IUserRepository userServices;
        IProductRepository productServices;
        ICategoryRepository categoryServices;
        public UserController(IUserRepository _userServices, IOrderRepository _order,IProductRepository _productServices , ICategoryRepository _categoryServices)
        {
            userServices = _userServices;
            orderServices = _order;
            productServices = _productServices;
            categoryServices = _categoryServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {

            return View(new User());
        }
        [HttpPost]
        public IActionResult SaveNewUser(User user)
        {
            if (ModelState.IsValid)
            {
                if (userServices.GetUserByEmail(user.Email) == null)
                {
                    userServices.Create(user);
                    return RedirectToAction("Login");
                }
            }
            return View("SignUp", user);
        }
        [HttpGet]
        public IActionResult Login()
        {
          
            return View();
        }
        [HttpPost]
        public IActionResult ConfirmUser(LoginVM User)
        {


            User user = userServices.GetUserByEmail(User.Email);
            if (user != null && user.Password == User.Password)
            {
				HttpContext.Session.SetInt32("UserId", user.Id);
	            HttpContext.Session.SetString("UserName", user.UserName);

				if (orderServices.GetByUserId(user.Id) == null)
                {
                    Order order = new Order(user.Id);
                   
                    orderServices.Create(order);
                }


                return RedirectToAction("UserHome", "User");
            }

            return View("Login");
        }
        public IActionResult UserHome() 
        {
            ViewBag.IsAuth = true;
            
            ViewBag.UserName= HttpContext.Session.GetString("UserName");

            List<Product>list = productServices.getAll();
            return View(list);
        }
        //public IActionResult Login(string UserName, string Password)
        //{
        //    //        AdminServices adminServices = new AdminServices();

        //    if (ModelState.IsValid)
        //    {
        //        var datauser = userServices.GetByPass(Password);//context.Users.FirstOrDefault(U => U.Password == user.Password);
        //        var dataAdmin = adminServices.GetByPass(Password);//context.Admins.FirstOrDefault(U => U.Password == user.Password);
        //        if (datauser != null && datauser.Password == Password && datauser.UserName == UserName)
        //        {
        //            HttpContext.Session.SetString("Name", datauser.UserName);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else if (dataAdmin != null && dataAdmin.Password == Password && dataAdmin.UserName ==UserName)
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Invalid UserName or password");
        //        }

        //    }

        //    return View();
        //}
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "User");
        }
        public IActionResult Account()
        {
            int userId = (int)HttpContext.Session.GetInt32("UserId");
            User user = userServices.GetUserById(userId);

            return View(user);
        }
   
    }
}
