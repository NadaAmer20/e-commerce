using first.Models;
using first.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace first.Controllers
{
    public class OrderController : Controller
    {
        IOrderRepository orderServices;
        IUserRepository userServices;
        IProductRepository productServices;
        IOrderDetailRepository orderDetailServices;
        public OrderController(IProductRepository _productServices, IUserRepository _userServices, IOrderRepository _orderServices, IOrderDetailRepository _orderDetailServices)
        {
            productServices = _productServices;
            userServices = _userServices;
            orderServices = _orderServices;
            orderDetailServices = _orderDetailServices;

        }
        public IActionResult Index()
        {
            return View();
        }
        /// Edited   : 

        [HttpPost]
      
        public IActionResult AddtoOrder(int id,int Quantity)//id , productid//order //+ 1 
        {
            
            int UserId = (int)HttpContext.Session.GetInt32("UserId");//
            Order order = orderServices.GetByUserId(UserId);//old
            Product product = productServices.GetById(id);//pr


            if (orderServices.GetOrderDetailByProductIdAndOrderId(order.Id, product.Id) == null)
            {
                OrderDetail orderDetail = new OrderDetail(order.Id, product.Id);
                orderDetail.Quantity += Quantity;

                orderDetailServices.Create(orderDetail);//add 



                order.TotalPrice += (Quantity * product.Price);//200
                order.TotalAmount+=Quantity;//10
                orderServices.Update(order.Id, order);

                return RedirectToRoute(new { controller = "Product", action = "ShowProduct", id = product.Id });



            }
            else//string : unitprice
            {
                ///10
                OrderDetail orderDetail = orderDetailServices.GetByOrderIdAndProductId(order.Id,product.Id);
                int oldQuantity = orderDetail.Quantity;
                orderDetail.Quantity = 0;
                orderDetail.Quantity+=Quantity;
                orderDetailServices.Update(orderDetail.Id, orderDetail);


                order.TotalPrice -= (oldQuantity * product.Price);
               order.TotalPrice += (Quantity * product.Price);
                order.TotalAmount -= oldQuantity;
                order.TotalAmount += Quantity;

                orderServices.Update(order.Id, order);

                return RedirectToRoute(new { controller = "Product", action = "ShowProduct", id = product.Id });



                // orderDetail.UnitPrice += product.Price;
            }
        }

        [HttpPost]
            public IActionResult Plus(int id)//id , productid//order //+ 1 
            {

                int UserId = (int)HttpContext.Session.GetInt32("UserId");//
                Order order = orderServices.GetByUserId(UserId);//old
                Product product = productServices.GetById(id);//pr


               
                    ///10
                    OrderDetail orderDetail = orderDetailServices.GetByOrderIdAndProductId(order.Id, product.Id);
                  
                orderDetail.Quantity++;
                orderDetailServices.Update(orderDetail.Id, orderDetail);


                order.TotalPrice += product.Price;

                   order.TotalAmount++;

                    orderServices.Update(order.Id, order);
                 
                    
                     return Json(new { done = true, quantity = orderDetail.Quantity});


            }
        [HttpPost]
           public IActionResult Minus(int id)//id , productid//order //+ 1 
            {

                int UserId = (int)HttpContext.Session.GetInt32("UserId");//
                Order order = orderServices.GetByUserId(UserId);//old
                Product product = productServices.GetById(id);//pr


               
                    ///10
                    OrderDetail orderDetail = orderDetailServices.GetByOrderIdAndProductId(order.Id, product.Id);
                  
                orderDetail.Quantity--;
                orderDetailServices.Update(orderDetail.Id, orderDetail);


                order.TotalPrice -= product.Price;

                order.TotalAmount--;

                    orderServices.Update(order.Id, order);


            return Json(new { done = true, quantity = orderDetail.Quantity });



        }



     
        public IActionResult DeleteOrderDetail(int id)//p.id 
            {


            int UserId = (int)HttpContext.Session.GetInt32("UserId");//
            Order order = orderServices.GetByUserId(UserId);//old
            Product product = productServices.GetById(id);//pr

           
           //string : unitprice
            
                OrderDetail orderDetail = orderDetailServices.GetByOrderIdAndProductId(order.Id, product.Id);
               orderDetail.Quantity--;
              orderDetailServices.Update(orderDetail.Id, orderDetail);
                // orderDetail.UnitPrice += product.Price;
         

            order.TotalPrice -= product.Price;
            order.TotalAmount--;
            orderServices.Update(order.Id, order);
            return RedirectToRoute(new { controller = "Product", action = "ShowProduct", id = product.Id });


            //update order 

        }
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            orderDetailServices.Delete(id);
            return Json(new { done=true, });
        }

        public IActionResult MyOrder()
         {
            int UserId = (int)HttpContext.Session.GetInt32("UserId");
            // int Userid = userServices.GetById(UserId).Id;
            List<OrderDetail>List = orderServices.GetOrderDetailByOrderId(UserId);//all orderDetail
            List<OrderDetail> dic = orderDetailServices.GetProductByOrderDetails(List);//all orderDetail + product (include)


            return View(dic);
         }
    }
}
// order  