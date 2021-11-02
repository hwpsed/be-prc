using LaundryApp.Data;
using LaundryApp.Model;
using LaundryApp.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryApp.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateOrder(Order order)
        {
            order.isDeleted = false;
            order.OrderId = GetOrdersForCreate().Count + 1;
            _db.Orders.Add(order);
            return Save();
        }

        public bool DeleteOrder(Order order)
        {
            order.isDeleted = true;
            _db.Orders.Update(order);
            return Save();
        }

        public Order GetOrder(int orderId)
        {
            return _db.Orders.FirstOrDefault(a => a.OrderId == orderId && !a.isDeleted);
        }

        public ICollection<Order> GetOrdersByAccountId (int accountId)
        {
            var query = from order in _db.Orders
                        where order.AccountId == accountId && !order.isDeleted
                        orderby order.OrderId
                        select order;
            return query.ToList();
        }

        public ICollection<Order> GetOrders()
        {
            return _db.Orders.Where(a => !a.isDeleted).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool OrderExist(int id)
        {
            return _db.Orders.Any(a => a.OrderId == id && !a.isDeleted);
        }

        public bool UpdateOrder(Order order)
        {
            order.isDeleted = true;
            _db.Orders.Update(order);
            return Save();
        }

        public ICollection<Order> GetOrdersForCreate()
        {
            return _db.Orders.ToList();
        }
    }
}
