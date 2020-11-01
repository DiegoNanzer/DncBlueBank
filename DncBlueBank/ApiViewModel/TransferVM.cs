using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DncBlueBank.ApiViewModel
{
    public class TransferVM
    {
        public string FromAgency { get; set; }
        public string FromNumber { get; set; }
        public string ToAgency { get; set; }
        public string ToNumber { get; set; }
        public decimal TransacionValue { get; set; }
    }
}
