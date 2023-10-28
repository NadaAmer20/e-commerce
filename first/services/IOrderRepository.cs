using first.Models;
using System.Collections.Generic;

namespace first.services
{
    public interface IOrderRepository
    {
        int Create(Order order);
        int Delete(int id);
        List<Order> getAll();
        Order GetByUserId(int userid);
        int Update(int id, Order order);
        OrderDetail GetOrderDetailByProductIdAndOrderId(int orderId, int productid);
        List<OrderDetail> GetOrderDetailByOrderId(int orderId);
    }
}