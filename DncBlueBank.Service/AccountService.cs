using DncBlueBank.Model;
using DncBlueBank.Model.Interfaces.Repositories;
using DncBlueBank.Model.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DncBlueBank.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;

        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }

        public async Task<AccountModel> FindAsync(int id) => await _repo.FindAsync(id);


        public async Task<IEnumerable<AccountModel>> FindAllAsync() => await _repo.FindAllAsync();

        public async Task<AccountModel> InsertAsync(AccountModel account)
        {
            // Validation

            return await _repo.InsertAsync(account);
        }

        public async Task<bool> UpdateAsync(AccountModel account)
        {
            // Validation

            return await _repo.UpdateAsync(account);
        }
    }
}
