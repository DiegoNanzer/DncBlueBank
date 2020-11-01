using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DncBlueBank.Model.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<AccountTransaction>> FindAllAsync(int accountId);

        Task InsertAsync(AccountTransaction transaction);
    }
}
