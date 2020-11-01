using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DncBlueBank.Model.Interfaces.Services
{
    public interface ITransactionBase
    {
        Task ExecAsync(string accountAgency, string accountNumber, decimal value);

        Task ExecWithNoTransactionAsync(string accountAgency, string accountNumber, decimal value);
    }
}
