using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DncBlueBank.ApiViewModel;
using DncBlueBank.Model;
using DncBlueBank.Model.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DncBlueBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountSvc;

        public AccountController(IAccountService accountSvc)
        {
            this._accountSvc = accountSvc;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _accountSvc.FindAllAsync());
            }
            catch (Exception error)
            {
                return BadRequest(new ErrorMessage(error.Message));
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccount(int id)
        {
            try
            {
                var account = await _accountSvc.FindAsync(id);

                if (account == null)
                    return NotFound();
                else
                    return Ok(account);
            }
            catch (Exception error)
            {
                return BadRequest(new ErrorMessage(error.Message));
            }
        }


        [ProducesResponseType(typeof(AccountModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] AccountModel account)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(GetErrorModel());
                else
                {
                    var inserted = await _accountSvc.InsertAsync(account);

                    var result201 = new ObjectResult(inserted);
                    result201.StatusCode = (int)System.Net.HttpStatusCode.Created;

                    return result201;
                }
            }
            catch (Exception error)
            {
                return BadRequest(new ErrorMessage(error.Message));
            }
        }

        [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] AccountModel account)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(GetErrorModel());
                else
                {
                    var updated = await _accountSvc.UpdateAsync(account);

                    if (updated)
                        return Ok();
                    else
                        return BadRequest(new ErrorMessage("Account not updated"));
                }
            }
            catch (Exception error)
            {
                return BadRequest(new ErrorMessage(error.Message));
            }
        }

    }
}
