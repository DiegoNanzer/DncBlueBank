using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DncBlueBank.Model.Interfaces.Services
{
    public interface ITransferService
    {
        Task Exec(string fromAccountAgency, string fromAccountNumber,
                  string toAccountAgency, string toAccountNumber,
                  decimal value);
    }
}
