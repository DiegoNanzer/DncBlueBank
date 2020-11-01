using System;
using System.Collections.Generic;
using System.Text;

namespace DncBlueBank.Model
{
    public class AccountTransaction
    {
        public int Id { get; set; }

        public int AccountID { get; set; }

        public eTransactionType Type { get; set; }

        public decimal Value { get; set; }

        public DateTime DateTime { get; set; }

    }
}
