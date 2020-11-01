using DncBlueBank.Model;
using DncBlueBank.Service;
using DncBlueBank.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;

namespace DncBlueBank.Test
{
    public class AccountDepositTest
    {

        private readonly AccountModel _account;

        public AccountDepositTest()
        {
            _account = new AccountModel()
            {
                Id = 1,
                Agency = "1",
                Number = "123",
                Balance = 1
            };
        }


        [Fact]
        public void CalculeBalance_Should_Throw_BusinessException()
        {
            #region Arrenge

            ICalculator deposit = new AccountDeposit(_account);

            #endregion

            #region Assert

            Assert.Throws<BusinessException>(() => deposit.CalculeBalance(0));


            #endregion
        }

        [Fact]
        public void CalculeBalance_Should_Throw_ArgumentNullException() =>
             Assert.Throws<ArgumentNullException>(() => new AccountDeposit(null));

        [Fact]
        public void CalculeBalance_Should_Works()
        {
            #region Arrenge
            _account.Balance = 100M;

            ICalculator deposit = new AccountDeposit(_account);

            #endregion

            #region Assert

            var act = deposit.CalculeBalance(100);


            #endregion

            act.Should().Be(200);
        }

    }
}
