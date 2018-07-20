using System;

namespace Moneybox.App
{
    public class Account
    {
        public const decimal PayInLimit = 4000m;
        public const decimal LowFundsThreshold = 500m;
        public const decimal PayInNotificationThreshold = 500m;

        public Guid Id { get; set; }

        public User User { get; set; }

        public decimal Balance { get; set; }

        public decimal Withdrawn { get; set; }

        public decimal PaidIn { get; set; }


        public bool IsInsufficientFunds(decimal amountToDeduct)
        {
            return ((this.Balance - amountToDeduct) < 0m) ? true : false;
        }

        public bool AreFundsLow(decimal amountToDeduct)
        {
            return ((this.Balance - amountToDeduct) < LowFundsThreshold) ? true : false;
        }

        public bool IsPayInLimitReached(decimal amountToPayIn)
        {
            return ((this.PaidIn + amountToPayIn) > PayInLimit) ? true : false;
        }

        public bool IsApproachingPayInLimit(decimal amountToPayIn)
        {
            return ((PayInLimit - (this.PaidIn + amountToPayIn)) < PayInNotificationThreshold) ? true : false;
        }

        public void WithdrawFunds(decimal amount)
        {
            this.Balance = this.Balance - amount;
            this.Withdrawn = this.Withdrawn - amount;
        }

        public void PayInFunds(decimal amount)
        {
            this.Balance = this.Balance + amount;
            this.PaidIn = this.PaidIn + amount;
        }
    }
}
