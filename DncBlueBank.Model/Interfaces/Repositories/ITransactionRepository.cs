using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DncBlueBank.Model.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable< AccountTransaction>> FindAsync(int accountId);
       
        Task<AccountTransaction> InsertAsync(AccountTransaction accountTransaction);
    }
}
