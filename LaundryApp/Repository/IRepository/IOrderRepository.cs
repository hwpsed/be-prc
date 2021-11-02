using LaundryApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryApp.Repository.IRepository
{
    public interface IOrderRepository
    {
        ICollection<Order> GetOrdersByAccountId(int accountId);
        bool CreateOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(Order order);
        ICollection<Order> GetOrders();
        ICollection<Order> GetOrdersForCreate();
        Order GetOrder(int orderId);
        bool OrderExist(int id);
        bool Save();
    }
}
