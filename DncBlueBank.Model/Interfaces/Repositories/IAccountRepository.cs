using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DncBlueBank.Model.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<AccountModel> FindAsync(int id);

        Task<AccountModel> FindAsync(string agency, string number);

        Task<IEnumerable<AccountModel>> FindAllAsync();

        Task<AccountModel> InsertAsync(AccountModel account);

        Task<bool> UpdateAsync(AccountModel account);
    }
}
