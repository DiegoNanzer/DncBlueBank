using DncBlueBank.Model;
using DncBlueBank.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DncBlueBank.Service
{
    public class AccountDeposit : ICalculator
    {
        private readonly AccountModel _account;

        public AccountDeposit(AccountModel account)
        {
            _account = account ?? throw new ArgumentNullException(nameof(account));
        }

        public decimal CalculeBalance(decimal value)
        {
            if (value <= 0)
                throw new BusinessException("Value can not be zero or negative");

            return _account.Balance + value;
        }
    }
}
