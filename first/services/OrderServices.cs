using first.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace first.services
{
    public class OrderServices : IOrderRepository
    {
        Context context;
        public OrderServices(Context _context)
        {
            context = _context;
        }
        public List<Order> getAll()
        {
            return context.Orders.ToList();
        }

        public int Create(Order order)
        {
            order.OrderDate = DateTime.Now;
               order.Status = "success";
            order.TotalAmount = 0;
            order.TotalPrice = 0;
               context.Orders.Add(order);
               int raw = context.SaveChanges();
               return raw;
        }
        public int Update(int id, Order order)
        {

            Order OldOrder = context.Orders.FirstOrDefault(u => u.Id == id);
          //  OldOrder.TotalPrice = order.TotalPrice;
           //OldOrder.OrderDate = order.OrderDate;
            OldOrder.Status = order.Status;
            OldOrder.TotalAmount = order.TotalAmount;
           // OldOrder.TotalPrice = order.TotalPrice;

            int raw = context.SaveChanges();
            return raw;
        }
        public int Delete(int id)
        {
            Order ordeer = context.Orders.FirstOrDefault(u => u.Id == id);
            context.Orders.Remove(ordeer);
            int raw = context.SaveChanges();
            return raw;
        }
        // Order => OD1 , OD2 , OD3
        public void Create(OrderDetail newOrder)
        {
            throw new System.NotImplementedException();
        }

        public Order GetByUserId(int userid)
        {
            return context.Orders.FirstOrDefault(u => u.UserId == userid);
        }
        public OrderDetail GetOrderDetailByProductIdAndOrderId(int orderId , int productId)
        {
			// return context.OrderDetails.FirstOrDefault(p => p.OrderId==orderId &&  p.ProductId==productId);
			// var x = context.OrderDetails.Where(o => o.ProductId == productId && o.OrderId == orderId).FirstOrDefault();
			//List<OrderDetail> orderDetails = context.OrderDetails.ToList();
			//var x = orderDetails.Where(o => o.OrderId == orderId).FirstOrDefault(p => p.ProductId == productId);
		   return context.OrderDetails.Where(o => o.OrderId == orderId && o.ProductId == productId).FirstOrDefault();
		}
        public List<OrderDetail> GetOrderDetailByOrderId(int UserId)
        {
            return context.Orders.Include(od => od.OrderDetails).FirstOrDefault(o => o.UserId == UserId).OrderDetails.ToList();
        }

    }
}
