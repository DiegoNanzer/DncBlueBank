using DncBlueBank.Model;
using DncBlueBank.Model.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DncBlueBank.DAL
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IConnectionFactory _factory;

        public TransactionRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<AccountTransaction>> FindAsync(int accountId)
        {
            using (IDbConnection con = _factory.CreateConnection())
            {
                string query =
                    $"Select * From Transactions" +
                    $" where" +
                    $" AccountID = @{nameof(AccountTransaction.AccountID)} ";

                return await con.QueryAsync<AccountTransaction>(query, new { AccountID = accountId });
            }
        }

        public async Task<AccountTransaction> InsertAsync(AccountTransaction accountTransaction)
        {
            using (IDbConnection con = _factory.CreateConnection())
            {
                accountTransaction.DateTime = DateTime.Now;

                var id = await con.ExecuteScalarAsync<int>(InsertQuery(), accountTransaction);

                if (id > 0)
                    accountTransaction.Id = id;

                return accountTransaction;
            }
        }

        private static string InsertQuery()
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine(" INSERT INTO[dbo].[Transactions] ");
            query.AppendLine("           ([AccountID] ");
            query.AppendLine("           ,[Type] ");
            query.AppendLine("           ,[Value] ");
            query.AppendLine("           ,[DateTime]) ");
            query.AppendLine(" VALUES ");
            query.AppendLine($"       ( @{nameof(AccountTransaction.AccountID)} ");
            query.AppendLine($"       ,@{nameof(AccountTransaction.Type)} ");
            query.AppendLine($"       ,@{nameof(AccountTransaction.Value)} ");
            query.AppendLine($"       ,@{nameof(AccountTransaction.DateTime)} ) ");
            query.AppendLine(" Select SCOPE_IDENTITY()");


            return query.ToString();
        }
    }
}
