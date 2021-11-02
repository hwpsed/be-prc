using AutoMapper;
using LaundryApp.Model;
using LaundryApp.Model.ViewEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryApp.Mapper
{
    public class PaymentMapper : Profile
    {
        public PaymentMapper()
        {
            CreateMap<Payment, ViewPayment>().ReverseMap();
        }
    }
}
