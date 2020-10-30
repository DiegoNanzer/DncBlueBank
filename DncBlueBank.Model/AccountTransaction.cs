using System;
using System.Collections.Generic;
using System.Text;

namespace DncBlueBank.Model
{
    public class AccountTransaction
    {
        public int Id { get; set; }

        public int FromAccountID { get; set; }

        public int? ToAccountID { get; set; }

        public eTransactionType Type { get; set; }

        public decimal Value { get; set; }

        public DateTime DateTime { get; set; }

        public AccountModel FromAccount { get; set; }
        public AccountModel ToAccount { get; set; }

    }
}
