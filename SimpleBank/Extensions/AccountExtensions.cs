namespace SimpleBank.Extensions
{
    internal static class AccountExtensions
    {
        internal static bool HasSufficientFunds(this Account account, Money amount)
        {
            return account.Amount.Value >= amount.Value;
        }
    }
}
