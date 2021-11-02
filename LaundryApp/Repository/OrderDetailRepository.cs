using LaundryApp.Data;
using LaundryApp.Model;
using LaundryApp.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryApp.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderDetailRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateOrderDetail(OrderDetail orderDetail)
        {
            orderDetail.OrderDetailId = GetOrderDetailsForCreate().Count + 1;
            orderDetail.Status = true;
            _db.OrderDetails.Add(orderDetail);
            return Save();
        }

        public bool DeleteOrderDetail(OrderDetail orderDetail)
        {
            orderDetail.Status = false;
            _db.OrderDetails.Update(orderDetail);
            return Save();
        }

        public OrderDetail GetOrderDetail(int orderDetailId)
        {
            return _db.OrderDetails.FirstOrDefault(a => a.OrderDetailId == orderDetailId && a.Status);
        }

        public ICollection<OrderDetail> GetOrderDetails()
        {
            return _db.OrderDetails.Where(a => a.Status).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool OrderDetailExist(int id)
        {
            return _db.OrderDetails.Any(a => a.OrderDetailId == id && a.Status);
        }

        public bool UpdateOrderDetail(OrderDetail orderDetail)
        {
            orderDetail.Status = true;
            _db.OrderDetails.Update(orderDetail);
            return Save();
        }

        public ICollection<OrderDetail> GetOrderDetailsForCreate()
        {
            return _db.OrderDetails.ToList();
        }
    }
}
