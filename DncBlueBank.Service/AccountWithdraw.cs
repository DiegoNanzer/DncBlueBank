using DncBlueBank.Model;
using DncBlueBank.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DncBlueBank.Service
{
    public class AccountWithdraw : ICalculator
    {
        private readonly AccountModel _account;

        public AccountWithdraw(AccountModel account)
        {
            _account = account ?? throw new ArgumentNullException(nameof(account));
        }

        public decimal CalculeBalance(decimal value)
        {
            if (value <= 0)
                throw new BusinessException("Value can not be zero or negative");

            var balance = _account.Balance - value;

            if(balance < 0)
                throw new BusinessException("Balance will be negative");

            return balance;
        }
    }
}
