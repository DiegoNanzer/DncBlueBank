using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DncBlueBank.Model.Interfaces.Repositories
{
    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
