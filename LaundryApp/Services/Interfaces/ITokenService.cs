using LaundryApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryApp.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Account account);
    }
}
