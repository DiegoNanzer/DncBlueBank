using DncBlueBank.Model.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DncBlueBank.Infra
{
    public class SqlConnectionFactory : IConnectionFactory
    {
        private readonly string strConnection;

        public SqlConnectionFactory(string strConnection)
        {
            this.strConnection = strConnection;
        }

        public IDbConnection CreateConnection() => new SqlConnection(strConnection);
    }
}
