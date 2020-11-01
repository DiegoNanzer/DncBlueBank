using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DncBlueBank.Controllers
{
    public class BaseController : ControllerBase
    {

        protected ErrorMessage GetErrorModel() =>
            new ErrorMessage(string.Concat(ModelState.Values.SelectMany(s =>
                            s.Errors.Select(e => e.ErrorMessage)), ','));

    }
}
