using DncBlueBank.Model;
using DncBlueBank.Model.Interfaces.Repositories;
using DncBlueBank.Model.Interfaces.Services;
using DncBlueBank.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DncBlueBank.Service
{
    public class WithdrawService : AccountTransactionBaseService, IWithdrawService
    {

        public WithdrawService(IAccountRepository accountRepo, ITransactionService trans) : base(accountRepo, trans)
        {
        }

        protected override ICalculator GetCalculator(AccountModel account) => new AccountWithdraw(account);
    }
}
