using AutoMapper;
using LaundryApp.Model;
using LaundryApp.Model.ViewEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryApp.Mapper
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            CreateMap<Order, ViewOrder>().ReverseMap();
        }
    }
}
