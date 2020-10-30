using System;
using System.Collections.Generic;
using System.Text;

namespace DncBlueBank.Model
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string Agency { get; set; }
        public string Number { get; set; }
        public decimal Balance { get; set; }
        public string Owner { get; set; }

    }
}
