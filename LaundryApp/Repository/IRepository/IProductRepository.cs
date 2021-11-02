using LaundryApp.Model;
using System.Collections.Generic;

namespace LaundryApp.Repository.IRepository
{
    public interface IProductRepository
    {
        bool CreateProduct(Product Product);
        ICollection<Product> GetProductByOrderId(int orderId);
        bool UpdateProduct(Product Product);
        bool DeleteProduct(Product Product);
        ICollection<Product> GetProducts();
        ICollection<Product> GetProductsForCreate();
        Product GetProduct(int ProductId);
        bool ProductExist(int id);
        bool Save();
    }
}
