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
    [Route("api/Account/Transfer")]
    public class TransferController : BaseController
    {
        private readonly ITransferService transferSvc;

        public TransferController(ITransferService transferSvc)
        {
            this.transferSvc = transferSvc;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] TransferVM transfer)
        {
            try
            {
                await transferSvc.Exec(
                    fromAccountAgency: transfer.FromAgency,
                    fromAccountNumber: transfer.FromNumber,
                    toAccountAgency: transfer.ToAgency, 
                    toAccountNumber: transfer.ToNumber, 
                    value: transfer.TransacionValue);
                return Ok();
            }
            catch (Exception error)
            {
                return BadRequest(new ErrorMessage(error.Message));
            }
        }
    }
}
