using System;
using System.Collections.Generic;
using System.Text;

namespace DncBlueBank.Model.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        IEnumerable<AccountModel> FindAll();
    }
}
