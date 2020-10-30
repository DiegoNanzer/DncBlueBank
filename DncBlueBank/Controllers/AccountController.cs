using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DncBlueBank.ApiView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DncBlueBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private static readonly IEnumerable<AccountViewModel> mock = new List<AccountViewModel>
        {
            new AccountViewModel { Agency = 1, Number = 1, Owner = "Diego", Balance = 2458.02M },
            new AccountViewModel { Agency = 2, Number = 2, Owner = "Roberto", Balance = 24523.02M },
            new AccountViewModel { Agency = 3, Number = 1, Owner = "Maria", Balance = 2458.01M },
            new AccountViewModel { Agency = 5, Number = 2, Owner = "Tuane", Balance = 245343.0212M },
        };

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<AccountViewModel>> Get()
        {
            return Ok(mock);
        }
    }
}
