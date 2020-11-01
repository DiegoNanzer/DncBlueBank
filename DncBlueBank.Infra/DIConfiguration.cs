using DncBlueBank.DAL;
using DncBlueBank.Model.Interfaces.Repositories;
using DncBlueBank.Model.Interfaces.Services;
using DncBlueBank.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DncBlueBank.Infra
{
    public static class DIConfiguration
    {
        private static string StrConnection { get; set; }

        public static void SetStringConnection(string strConnetion)
        {
            StrConnection = strConnetion;
        }

        public static void Mapper(IServiceCollection service)
        {
            MapperServices(service);
            MapperRepositories(service);
        }

        private static void MapperServices(IServiceCollection service)
        {
            service.AddTransient<IAccountService, AccountService>();
            service.AddTransient<IDepositService, DepositService>();
            service.AddTransient<IWithdrawService, WithdrawService>();
            service.AddTransient<ITransferService, TransferService>();
            service.AddTransient<ITransactionService, TransactionService>();
        }

        private static void MapperRepositories(IServiceCollection service)
        {
            service.AddSingleton<IConnectionFactory>(x => new SqlConnectionFactory(StrConnection));
            service.AddTransient<IAccountRepository, AccountRepository>();
            service.AddTransient<ITransactionRepository, TransactionRepository>();
        }
    }
}
