using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DncBlueBank.Model.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AccountModel> FindAsync(int id);

        Task<IEnumerable<AccountModel>> FindAllAsync();

        Task<AccountModel> InsertAsync(AccountModel account);

        Task<bool> UpdateAsync(AccountModel account);
    }
}
