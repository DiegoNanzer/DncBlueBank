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
    public abstract class AccountTransactionBaseService
    {
        protected readonly IAccountRepository _accountRepo;
        protected readonly ITransactionService _trans;

        protected AccountTransactionBaseService(IAccountRepository accountRepo, ITransactionService trans)
        {
            _accountRepo = accountRepo;
            _trans = trans;
        }

        public virtual async Task ExecAsync(string accountAgency, string accountNumber, decimal value)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await ExecWithNoTransactionAsync(accountAgency, accountNumber, value);

                transaction.Complete();
            }
        }

        public async Task ExecWithNoTransactionAsync(string accountAgency, string accountNumber, decimal value)
        {
            var account = await _accountRepo.FindAsync(accountAgency, accountNumber) ?? throw new BusinessException("Account not find");

            ICalculator calculator = GetCalculator(account);

            account.Balance = calculator.CalculeBalance(value);

            await _accountRepo.UpdateAsync(account);

            await CreateTransactionLog(account.Id, value, GetTransactionType(calculator));
        }

        private eTransactionType GetTransactionType(ICalculator calculator)
        {
            return (calculator is AccountDeposit) ? eTransactionType.DEPOSIT : eTransactionType.WITHDRAW; 
        }

        private async Task CreateTransactionLog(int accountID, decimal value, eTransactionType type)
        {
            AccountTransaction transModel = new AccountTransaction
            {
                AccountID = accountID,
                Type = type,
                Value = value
            };

            await _trans.InsertAsync(transModel);
        }

        protected abstract ICalculator GetCalculator(AccountModel account);
    }
}
