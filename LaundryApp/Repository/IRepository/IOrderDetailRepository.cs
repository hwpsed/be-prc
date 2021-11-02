using LaundryApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryApp.Repository.IRepository
{
    public interface IOrderDetailRepository
    {
        bool CreateOrderDetail(OrderDetail orderDetail);
        bool UpdateOrderDetail(OrderDetail orderDetail);
        bool DeleteOrderDetail(OrderDetail orderDetail);
        ICollection<OrderDetail> GetOrderDetails();
        ICollection<OrderDetail> GetOrderDetailsForCreate();
        OrderDetail GetOrderDetail(int orderDetail);
        bool OrderDetailExist(int id);
        bool Save();
    }
}
