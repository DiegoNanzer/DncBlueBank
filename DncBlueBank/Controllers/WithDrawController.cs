using DncBlueBank.ApiViewModel;
using DncBlueBank.Model.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DncBlueBank.Controllers
{
    [ApiController]
    [Route("api/Account/Withdraw")]
    public class WithDrawController : BaseController
    {
        private readonly IWithdrawService _withdrawSvc;

        public WithDrawController(IWithdrawService withdrawSvc)
        {
            _withdrawSvc = withdrawSvc;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] TransactionVM trans)
        {
            try
            {
                await _withdrawSvc.ExecAsync(trans.Agency, trans.Number, trans.TransacionValue);
                return Ok();
            }
            catch (Exception error)
            {
                return BadRequest(new ErrorMessage(error.Message));
            }
        }
    }
}
