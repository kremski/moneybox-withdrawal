using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;

        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        { 
            var from = this.accountRepository.GetAccountById(fromAccountId);
            
            if (from.IsInsufficientFunds(amount))
            {
                throw new InvalidOperationException("Insufficient funds to withdraw money");
            }

            if (from.AreFundsLow(amount))
            {
                this.notificationService.NotifyFundsLow(from.User.Email);
            }

            from.WithdrawFunds(amount);

            this.accountRepository.Update(from);
        }
    }
}
