using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moneybox.App;
using System;

namespace UnitTests
{
    [TestClass]
    public class AccountTests
    {
        private Account fromAccount;
        private Account toAccount;
        private decimal amount;

        [TestInitialize]
        public void Setup()
        {
            fromAccount = new Account
            {
                Id = Guid.Parse("77687AA3-AD2C-4052-9B26-F7DD3829744B"),
                User = new User(),
                Balance = 5000m,
                Withdrawn = 3000m,
                PaidIn = 3000m
            };
            toAccount = new Account
            {
                Id = Guid.Parse("E28C44A3-103C-4E73-9949-31DF0FD98663"),
                User = new User(),
                Balance = 1000m,
                Withdrawn = 0m,
                PaidIn = 2000m
            };
        }


        [TestMethod]
        public void IsInsufficientFundsReturnsTrueWhenBalanceNegative()
        {
            //Arrange
            amount = 6000m;

            //Act
            var result = fromAccount.IsInsufficientFunds(amount);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsInsufficientFundsReturnsFalseWhenBalancePositive()
        {
            //Arrange
            amount = 3000m;

            //Act
            var result = fromAccount.IsInsufficientFunds(amount);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AreFundsLowReturnsTrueWhenBelowLowFundsThreshold()
        {
            //Arrange
            amount = 4600m;

            //Act
            var result = fromAccount.AreFundsLow(amount);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AreFundsLowReturnsFalseWhenAboveLowFundsThreshold()
        {
            //Arrange
            amount = 3000m;

            //Act
            var result = fromAccount.AreFundsLow(amount);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsPayInLimitReachedReturnsTrueWenAbovePayInLimit()
        {
            //Arrange
            amount = 3000m;

            //Act
            var result = toAccount.IsPayInLimitReached(amount);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPayInLimitReachedReturnsFalseWenBelowPayInLimit()
        {
            //Arrange
            amount = 1000m;

            //Act
            var result = toAccount.IsPayInLimitReached(amount);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsApproachingPayInLimitReturnsTrueWhenBelowPayInNotificationThreshold()
        {
            //Arrange
            amount = 1600m;

            //Act
            var result = toAccount.IsApproachingPayInLimit(amount);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsApproachingPayInLimitReturnsFalseWhenAbovePayInNotificationThreshold()
        {
            //Arrange
            amount = 1000m;

            //Act
            var result = toAccount.IsApproachingPayInLimit(amount);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
