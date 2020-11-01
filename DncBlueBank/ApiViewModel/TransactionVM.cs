using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DncBlueBank.ApiViewModel
{
    public class TransactionVM
    {
        public string Agency { get; set; }
        public string Number { get; set; }
        public decimal TransacionValue { get; set; }
    }
}
