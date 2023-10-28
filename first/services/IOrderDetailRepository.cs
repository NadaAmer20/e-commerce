using first.Models;
using System.Collections.Generic;

namespace first.services
{
    public interface IOrderDetailRepository
    {
        OrderDetail GetById(int id);
        List<OrderDetail> getAll();
        int Create(OrderDetail orderDetail);
        int Update(int id ,OrderDetail orderDetail);
        int Delete(int id);
        OrderDetail GetByOrderIdAndProductId(int orderid,int productId);
        List<OrderDetail> GetProductByOrderDetails(List<OrderDetail> List);



    }
}