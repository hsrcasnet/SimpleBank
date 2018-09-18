using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SimpleBank.Extensions;

namespace SimpleBank
{
    [DebuggerDisplay("Bank: Name={this.Name}")]
    public class Bank
    {
        private readonly ICollection<Account> accounts;
        public Bank(string name)
        {
            this.Name = name;
            this.accounts = new List<Account>();
        }

        public string Name { get; }

        /// <summary>
        /// Creates a new bank account for the given <see cref="customer"/>
        /// and deposits
        /// </summary>
        public Account CreateAccount(Person customer, Money initialDeposit)
        {
            if (ValidPersonWithdrawTransaction(customer, initialDeposit))
            {
                customer.Cash = new Money(customer.Cash.Value - initialDeposit.Value);
                var account = new Account(initialDeposit, customer);
                this.accounts.Add(account);
                return account;
            }

            return null;
        }

        /// <summary>
        /// Returns the account for given <see cref="accountId"/>
        /// <exception cref="Exception">If account cannot be found</exception>
        /// </summary>
        private Account GetAccountById(string accountId)
        {
            var account = this.accounts.SingleOrDefault(a => a.Id == accountId);
            if (account == null)
            {
                throw new Exception($"Account with Id={accountId} does not exist!");
            }

            return account;
        }

        /// <summary>
        /// Returns all accounts for a given <see cref="customer"/>.
        /// </summary>
        public IEnumerable<Account> GetAccounts(Person customer)
        {
            return this.accounts.Where(a => a.Owner.Id == customer.Id);
        }

        /// <summary>
        ///     Add the <see cref="amount" /> to given account with <see cref="targetAccountId" />.
        /// </summary>
        /// <param name="targetAccountId"></param>
        /// <param name="amount"></param>
        public void Deposit(string targetAccountId, Money amount)
        {
            if (ValidAccountDepositTransaction(amount))
            {
                var targetAccount = this.GetAccountById(targetAccountId);
                targetAccount.Amount = new Money(targetAccount.Amount.Value + amount.Value);
            }
        }

        /// <summary>
        ///     Extracts the <see cref="amount" /> from account with <see cref="sourceAccountId" />.
        /// </summary>
        /// <param name="sourceAccountId"></param>
        /// <param name="amount"></param>
        public void Withdraw(string sourceAccountId, Money amount)
        {
            var sourceAccount = this.GetAccountById(sourceAccountId);

            if (ValidAccountWithdrawTransaction(sourceAccount, amount))
            {
                sourceAccount.Amount = new Money(sourceAccount.Amount.Value - amount.Value);
            }
        }

        /// <summary>
        ///     Transfers the given <see cref="amount" /> from <see cref="sourceAccountId" /> to <see cref="targetAccountId" /> if
        ///     all validations succeed.
        /// </summary>
        public void Transfer(string sourceAccountId, string targetAccountId, Money amount)
        {
            this.Withdraw(sourceAccountId, amount);
            this.Deposit(targetAccountId, amount);
        }

        private static bool ValidPersonWithdrawTransaction(Person person, Money amount)
        {
            if (!amount.IsPositive())
            {
                throw new ArgumentException("Invalid value, Negative " + amount.Value);
            }

            if (!person.HasSufficientFunds(amount))
            {
                throw new ArgumentException("Person has insufficient funds: " + person.Cash.Value + " < " + amount.Value);
            }

            return true;
        }

        private static bool ValidAccountWithdrawTransaction(Account sourceAccount, Money amount)
        {
            if (!amount.IsPositive())
            {
                throw new ArgumentException("Invalid value, Negative " + amount.Value);
            }

            if (!sourceAccount.HasSufficientFunds(amount))
            {
                throw new ArgumentException("Account has insufficient funds: " + sourceAccount.Amount.Value + " < " + amount.Value);
            }

            return true;
        }

        private static bool ValidAccountDepositTransaction(Money amount)
        {
            if (!amount.IsPositive())
            {
                throw new ArgumentException("Invalid value, Negative " + amount.Value);
            }

            return true;
        }
    }
}