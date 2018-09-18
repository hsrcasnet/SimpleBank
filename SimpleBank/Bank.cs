using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SimpleBank
{
    [DebuggerDisplay("Bank: Name={this.Name}")]
    public class Bank
    {
        public Bank(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        /// <summary>
        ///     Creates a new bank account for the given <see cref="customer" />
        ///     and deposits
        /// </summary>
        public Account CreateAccount(Person customer, Money initialDeposit)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Returns the account for given <see cref="accountId" />
        ///     <exception cref="Exception">If account cannot be found</exception>
        /// </summary>
        private Account GetAccountById(string accountId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Returns all accounts for a given <see cref="customer" />.
        /// </summary>
        public IEnumerable<Account> GetAccounts(Person customer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Add the <see cref="amount" /> to given account with <see cref="targetAccountId" />.
        /// </summary>
        /// <param name="targetAccountId"></param>
        /// <param name="amount"></param>
        public void Deposit(string targetAccountId, Money amount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Extracts the <see cref="amount" /> from account with <see cref="sourceAccountId" />.
        /// </summary>
        /// <param name="sourceAccountId"></param>
        /// <param name="amount"></param>
        public void Withdraw(string sourceAccountId, Money amount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Transfers the given <see cref="amount" /> from <see cref="sourceAccountId" /> to <see cref="targetAccountId" /> if
        ///     all validations succeed.
        /// </summary>
        public void Transfer(string sourceAccountId, string targetAccountId, Money amount)
        {
            throw new NotImplementedException();
        }
    }
}