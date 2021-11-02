using System.Collections.Generic;
using AutoMapper;
using LaundryApp.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using LaundryApp.Model;
using LaundryApp.Model.ViewEntity;
using Microsoft.AspNetCore.Authorization;

namespace LaundryApp.Controllers
{
    public class AccountController : BaseApiController
    {
        private IAccountRepository _accountrepo;
        private readonly IMapper _mapper;

        public AccountController(IAccountRepository accountrepo, IMapper mapper)
        {
            _accountrepo = accountrepo;
            _mapper = mapper;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAccounts()
        {
            var objList = _accountrepo.GetAccounts();

            var objDtos = new List<ViewAccount>();

            foreach (var obj in objList)
            {
                objDtos.Add(_mapper.Map<ViewAccount>(obj));                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         

            }

            return Ok(objDtos);
        }

        [HttpGet("{accountId:int}")]
        public IActionResult GetAccount(int accountId)
        {
            var obj = _accountrepo.GetAccount(accountId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<ViewAccount>(obj);

            return Ok(objDto);
        }

        [AllowAnonymous]
        [HttpGet("byUsername/{Username}")]
        public IActionResult GetAccountByUsername(string Username)
        {
            var obj = _accountrepo.GetAccountByUsername(Username);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<ViewAccount>(obj);

            return Ok(objDto);
        }

        [HttpPost("Register")]
        public IActionResult CreateAccount([FromBody] ViewAccount viewAccount)
        {
            if (viewAccount == null)
            {
                return BadRequest(ModelState);
            }

            if (_accountrepo.AccountExistByUsername(viewAccount.Username))
            {
                ModelState.AddModelError("", "Account Type Exsits!");
                return StatusCode(500, ModelState);
            }

            var account = _accountrepo.CreateAccount(viewAccount);

            if (account == null)
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {viewAccount.AccountName}");
                return StatusCode(500, ModelState); 
            }

            return Ok(account);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] ViewAccount viewAccount)
        {
            var account = _accountrepo.GetAccountByUsername(viewAccount.Username);

            if (account == null)
                return Unauthorized("Invalid Username");

            if (!_accountrepo.GetPassword(viewAccount.Password, viewAccount.Username))
                return Unauthorized("Invalid Password");

            return Ok(account);
        }

        [HttpPut("{accountId:int}")]
        public IActionResult UpdateAccount(int accountId, [FromBody] ViewUpdateAccount viewUpdateAccount)
        {
            if (viewUpdateAccount == null || accountId != viewUpdateAccount.AccountId)
            {
                return BadRequest(ModelState);
            }

            var account = _accountrepo.GetAccount(accountId);

            _mapper.Map(viewUpdateAccount, account);

            var accountObj = _mapper.Map<Account>(account);

            if (!_accountrepo.UpdateAccount(accountObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {accountObj.AccountName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{accountId:int}")]
        public IActionResult DeleteAccount(int accountId)
        {

            if (!_accountrepo.AccountExist(accountId))
            {
                return NotFound();
            }

            var accountObj = _accountrepo.GetAccount(accountId);

            if (!_accountrepo.DeleteAccount(accountObj))
            {
                ModelState.AddModelError("", $"Something went wrong when Deleting the record {accountObj.AccountName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
