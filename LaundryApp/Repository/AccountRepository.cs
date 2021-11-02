using AutoMapper;
using LaundryApp.Data;
using LaundryApp.Interfaces;
using LaundryApp.Model;
using LaundryApp.Model.ViewEntity;
using LaundryApp.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace LaundryApp.Repository
{

    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AccountRepository(ApplicationDbContext db, IMapper mapper, ITokenService tokenService)
        {
            _db = db;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public bool AccountExistByUsername(string Username)
        {
            return _db.Accounts.Any(a => a.Username == Username && !a.isDeleted);
        }

        public bool AccountExist(int id)
        {
            return _db.Accounts.Any(a => a.AccountId == id && !a.isDeleted);
        }

        public ViewAccount CreateAccount(ViewAccount viewAccount)
        {
            using var hmac = new HMACSHA512();

            viewAccount.AccountId = GetAccountsForCreate().Count + 1;

            var account = _mapper.Map<Account>(viewAccount);

            account.isDeleted = false;

            account.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(viewAccount.Password));
            account.PasswordSalt = hmac.Key;

            _db.Accounts.Add(account);
            _db.SaveChanges();
            return viewAccount;
        }

        public bool DeleteAccount(Account account)
        {
            account.isDeleted = true;
            _db.Accounts.Update(account);
            return Save();
        }

        public Account GetAccount(int accountId)
        {
            return _db.Accounts.FirstOrDefault(a => a.AccountId == accountId && !a.isDeleted);
        }

        public ICollection<Account> GetAccounts()
        {
            return _db.Accounts.Where(a => !a.isDeleted).ToList();
        }

        public ViewAccount GetUsername(string Username)
        {
            var user = _db.Accounts
                .SingleOrDefault(x => x.Username == Username && !x.isDeleted);
            if (user == null) return null;
            var account = _mapper.Map<ViewAccount>(user);
            account.Token = _tokenService.CreateToken(user);
            return account;
        }

        public bool GetPassword(string password,string Username)
        {
            var user = _db.Accounts
                .SingleOrDefault(x => x.Username == Username && !x.isDeleted);
            if (user == null) return false;

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return false;
            }
            return true;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateAccount(Account account)
        {
            _db.Accounts.Update(account);
            return Save();
        }

        public Account GetAccountByUsername(string Username)
        {
            return _db.Accounts.FirstOrDefault(a => a.Username == Username && !a.isDeleted);
        }

        public ICollection<Account> GetAccountsForCreate()
        {
            return _db.Accounts.ToList();
        }
    }
}
