using DncBlueBank.Model;
using DncBlueBank.Model.Interfaces.Repositories;
using DncBlueBank.Model.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DncBlueBank.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transRepo;

        public TransactionService(ITransactionRepository transRepo)
        {
            _transRepo = transRepo;
        }

        public async Task<IEnumerable<AccountTransaction>> FindAllAsync(int accountId)
        {
            return await _transRepo.FindAsync(accountId);
        }

        public Task InsertAsync(AccountTransaction transaction)
        {
            return _transRepo.InsertAsync(transaction);
        }
    }
}
