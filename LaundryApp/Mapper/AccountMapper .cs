using AutoMapper;
using LaundryApp.Model;

using LaundryApp.Model.ViewEntity;
using System.Linq;

namespace LaundryApp.Mapper
{
    public class AccountMapper : Profile
    {
        public AccountMapper()
        {
            CreateMap<ViewAccount, Account>().ReverseMap();

            CreateMap<ViewUpdateAccount, Account>().ReverseMap();
        }
    }
}
