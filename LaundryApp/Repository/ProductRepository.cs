using LaundryApp.Data;
using LaundryApp.Model;
using LaundryApp.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaundryApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateProduct(Product product)
        {
            product.Status = true;
            product.ProductId = GetProductsForCreate().Count + 1;
            _db.Products.Add(product);
            return Save();
        }

        public bool DeleteProduct(Product Product)
        {
            Product.Status = false;
            _db.Products.Update(Product);
            return Save();
        }

        public Product GetProduct(int ProductId)
        {
            return _db.Products.FirstOrDefault(a => a.ProductId == ProductId && a.Status);
        }

        public ICollection<Product> GetProductByOrderId(int orderId)
        {
            var orderDetails = from orderDetail in _db.OrderDetails
                               where orderDetail.OrderId == orderId && orderDetail.Status
                               select orderDetail;
           List<Product> Products = new List<Product>();
           Product Product = new Product();
            foreach (var orderDetail in orderDetails)
            {
                Products.Add(GetProduct(orderDetail.ProductId));
            }
            return Products;
        }

        public ICollection<Product> GetProducts()
        {
            return _db.Products.Where(a => a.Status).ToList();
        }


        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool ProductExist(int id)
        {
            return _db.Products.Any(a => a.ProductId == id && a.Status);
        }

        public bool UpdateProduct(Product Product)
        {
            Product.Status = true;
            _db.Products.Update(Product);
            return Save();
        }

        public ICollection<Product> GetProductsForCreate()
        {
            return _db.Products.ToList();
        }
    }
}
