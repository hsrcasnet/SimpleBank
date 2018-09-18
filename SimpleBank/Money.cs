using System.Diagnostics;

namespace SimpleBank
{
    [DebuggerDisplay("Money: Value={this.Value}")]
    public class Money
    {
        public Money(decimal amount)
        {
            this.Value = amount;
        }

        public decimal Value { get; }
    }
}