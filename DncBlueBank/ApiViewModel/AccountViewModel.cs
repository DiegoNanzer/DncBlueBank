using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DncBlueBank.ApiView
{
    public class AccountViewModel
    {
        public int Agency { get; set; }

        public int Number { get; set; }

        public string Owner { get; set; }

        public decimal Balance { get; set; }

    }
}
