using DncBlueBank.Model;
using DncBlueBank.Model.Interfaces.Repositories;
using DncBlueBank.Model.Interfaces.Services;
using DncBlueBank.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DncBlueBank.Service
{
    public class DepositService : AccountTransactionBaseService, IDepositService
    {
        public DepositService(IAccountRepository accountRepo, ITransactionService trans) : base(accountRepo, trans)
        {

        }

        protected override ICalculator GetCalculator(AccountModel account) => new AccountDeposit(account);

    }
}
