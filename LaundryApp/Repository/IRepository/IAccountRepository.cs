using LaundryApp.Model;
using LaundryApp.Model.ViewEntity;
using System.Collections.Generic;

namespace LaundryApp.Repository.IRepository
{
    public interface IAccountRepository
    {
        ICollection<Account> GetAccounts();
        ICollection<Account> GetAccountsForCreate();
        Account GetAccount(int accountId);
        Account GetAccountByUsername(string email);
        bool AccountExistByUsername(string email);
        bool AccountExist(int id);
        ViewAccount GetUsername(string email);
        bool GetPassword(string password, string email);
        ViewAccount CreateAccount(ViewAccount account);
        bool UpdateAccount(Account account);
        bool DeleteAccount(Account account);
        bool Save();
    }
}
