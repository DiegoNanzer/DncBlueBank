using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using DncBlueBank.Model;
using DncBlueBank.Service.Interfaces;
using DncBlueBank.Service;

namespace DncBlueBank.Test
{
    public class AccountWithdrawTest
    {

        private readonly AccountModel _account;

        public AccountWithdrawTest()
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

            ICalculator withdraw = new AccountWithdraw(_account);

            #endregion

            #region Assert

            Assert.Throws<BusinessException>(() => withdraw.CalculeBalance(0));
            
            #endregion
        }

        [Fact]
        public void CalculeBalance_Should_Throw_ArgumentNullException() =>
             Assert.Throws<ArgumentNullException>(() => new AccountWithdraw(null));

        [Fact]
        public void CalculeBalance_Should_Throw_BusinessException_When_Balance_Negative()
        {
            #region Arrenge
            _account.Balance = 200;

            ICalculator withdraw = new AccountWithdraw(_account);

            #endregion

            #region Assert

            Assert.Throws<BusinessException>(() => withdraw.CalculeBalance(300));

            #endregion
        }

        [Fact]
        public void CalculeBalance_Should_Works()
        {
            #region Arrenge
            _account.Balance = 100M;

            ICalculator withdraw = new AccountWithdraw(_account);

            #endregion

            #region Act

            var act = withdraw.CalculeBalance(50);

            #endregion

            #region Assert

            act.Should().Be(50);

            #endregion
        }
    }
}
