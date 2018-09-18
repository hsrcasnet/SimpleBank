using System;
using System.Diagnostics;

namespace SimpleBank
{
    [DebuggerDisplay("Account: Id={this.Id}, Person={this.Owner.Name}, Money={this.Amount.Value}")]
    public class Account
    {
        public Account(Money initialAmount, Person owner)
        {
            this.Id = $"CH00-{Guid.NewGuid().ToString("D").ToUpperInvariant()}";
            this.Amount = initialAmount;
            this.Owner = owner;
        }

        public string Id { get; }

        public Money Amount { get; internal set; }

        public Person Owner { get; }
    }
}