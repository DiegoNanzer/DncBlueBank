using DncBlueBank.Model.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DncBlueBank.Service
{
    public class TransferService : ITransferService
    {
        private IDepositService _depositSvc;
        private IWithdrawService _withdrawService;

        public TransferService(IDepositService depositSvc, IWithdrawService withdrawService)
        {
            _depositSvc = depositSvc;
            _withdrawService = withdrawService;
        }

        public async Task Exec(string fromAccountAgency, string fromAccountNumber, string toAccountAgency, string toAccountNumber, decimal value)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _withdrawService.ExecWithNoTransactionAsync(fromAccountAgency, fromAccountNumber, value);
                await _depositSvc.ExecWithNoTransactionAsync(toAccountAgency, toAccountNumber, value);

                transaction.Complete();
            }
        }
    }
}
