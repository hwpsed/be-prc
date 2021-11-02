using AutoMapper;
using LaundryApp.Model;
using LaundryApp.Model.ViewEntity;

namespace LaundryApp.Mapper
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ViewProduct>().ReverseMap();
        }
    }
}
