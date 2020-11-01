using DncBlueBank.Model;
using DncBlueBank.Model.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using System.Linq;

namespace DncBlueBank.DAL
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConnectionFactory _factory;

        public AccountRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<AccountModel> FindAsync(int id)
        {
            using (IDbConnection con = _factory.CreateConnection())
            {
                string query =
                    $"Select * From Accounts" +
                    $" where" +
                    $" Id = @{nameof(AccountModel.Id)} ";

                var result = await con.QueryAsync<AccountModel>(query, new { Id = id });

                return result.FirstOrDefault();
            }
        }

        public async Task<AccountModel> FindAsync(string agency, string number)
        {
            using (IDbConnection con = _factory.CreateConnection())
            {
                string query =
                    $"Select * From Accounts" +
                    $" where" +
                    $" Agency = @{nameof(AccountModel.Agency)} " +
                    $"and " +
                    $" Number =  @{nameof(AccountModel.Number)} ";

                var result = await con.QueryAsync<AccountModel>(query,
                    new
                    {
                        Agency = agency.Trim(),
                        Number = number.Trim()
                    });

                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<AccountModel>> FindAllAsync()
        {
            using (IDbConnection con = _factory.CreateConnection())
            {
                return await con.QueryAsync<AccountModel>("Select * From Accounts");
            }
        }

        public async Task<AccountModel> InsertAsync(AccountModel account)
        {
            using (IDbConnection con = _factory.CreateConnection())
            {
                var id = await con.ExecuteScalarAsync<int>(InsertQuery(), account);

                if (id > 0)
                    account.Id = id;

                return account;
            }
        }

        public async Task<bool> UpdateAsync(AccountModel account)
        {
            using (IDbConnection con = _factory.CreateConnection())
            {
                var rowAffect = await con.ExecuteAsync(UpdateQuery(), account);

                return rowAffect > 0;
            }
        }

        private static string InsertQuery()
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("INSERT INTO [dbo].[Accounts] ");
            query.AppendLine("       ([Agency] ");
            query.AppendLine("       ,[Number] ");
            query.AppendLine("       ,[Balance] ");
            query.AppendLine("       ,[Owner]) ");
            query.AppendLine("VALUES ");
            query.AppendLine($"      ( @{nameof(AccountModel.Agency)} ");
            query.AppendLine($"       ,@{nameof(AccountModel.Number)} ");
            query.AppendLine($"       ,@{nameof(AccountModel.Balance)} ");
            query.AppendLine($"       ,@{nameof(AccountModel.Owner)} ");
            query.AppendLine("        )");
            query.AppendLine(" Select SCOPE_IDENTITY()");

            return query.ToString();
        }

        private static string UpdateQuery()
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine(" UPDATE [dbo].[Accounts] ");
            query.AppendLine($"   SET [Agency]  = @{nameof(AccountModel.Agency)} ");
            query.AppendLine($"      ,[Number]  = @{nameof(AccountModel.Number)} ");
            query.AppendLine($"      ,[Balance] = @{nameof(AccountModel.Balance)} ");
            query.AppendLine($"      ,[Owner]   = @{nameof(AccountModel.Owner)} ");
            query.AppendLine($"  WHERE [id] = @{nameof(AccountModel.Id)} ");

            return query.ToString();
        }


    }
}
