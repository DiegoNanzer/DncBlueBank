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
    public class TransactionsController : BaseController
    {
        private readonly ITransactionService _transactionSvc;

        public TransactionsController(ITransactionService transactionSvc)
        {
            _transactionSvc = transactionSvc;
        }

        public ITransactionService TransactionSvc { get; }

        [HttpGet("api/Account/{id}/Transaction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _transactionSvc.FindAllAsync(id));
            }
            catch (Exception error)
            {
                return BadRequest(new ErrorMessage(error.Message));
            }
        }
    }
}
