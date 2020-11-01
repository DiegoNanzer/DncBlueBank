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
    public class DepositController : BaseController
    {
        private readonly IDepositService _depositSvc;

        public DepositController(IDepositService depositSvc)
        {
            _depositSvc = depositSvc;
        }

        [HttpPost("api/Account/Deposit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] TransactionVM trans)
        {
            try
            {
                await _depositSvc.ExecAsync(trans.Agency, trans.Number, trans.TransacionValue);

                return Ok( );
            }
            catch (Exception error)
            {
                return BadRequest(new ErrorMessage(error.Message));
            }
        }
    }
}
