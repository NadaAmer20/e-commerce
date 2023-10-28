using first.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace first.services
{
    public class OrderDetailServices:IOrderDetailRepository
    {
        
            Context context = new Context();
            public List<OrderDetail> getAll()
            {
                return context.OrderDetails.ToList();
            }
            public OrderDetail GetById(int id)
            {
                return context.OrderDetails.FirstOrDefault(o => o.Id == id);
            }
            public OrderDetail GetByOrderIdAndProductId(int orderid, int productId)
            {
                return context.OrderDetails.Where(o => o.OrderId == orderid && o.ProductId == productId).FirstOrDefault();
            }
            public int Create(OrderDetail orderDetail)
            {
               

                context.OrderDetails.Add(orderDetail);
                int raw = context.SaveChanges();
                return raw;
            }

            public int Delete(int id)
            {
                OrderDetail ordeer = context.OrderDetails.FirstOrDefault(u => u.Id == id);
                context.OrderDetails.Remove(ordeer);
                int raw = context.SaveChanges();
                return raw;
            }

            public int Update(int id , OrderDetail newOrderDetail)
            {
            OrderDetail oldOrderDetail = context.OrderDetails.FirstOrDefault(od => od.Id == id);
             oldOrderDetail.Quantity = newOrderDetail.Quantity;
            return context.SaveChanges();
            }
        public List<OrderDetail>  GetProductByOrderDetails(List<OrderDetail> List)
        {
         List<OrderDetail>list = new List<OrderDetail>();
            foreach (var item in List)
            {
                OrderDetail orderDetail = context.OrderDetails.Include(p => p.Product).FirstOrDefault(od => od.Id == item.Id);
                list.Add(orderDetail);
               
            }
            return list;
        }
       
    }
}
