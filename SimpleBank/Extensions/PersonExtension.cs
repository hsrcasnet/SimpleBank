namespace SimpleBank.Extensions
{
    internal static class PersonExtension
    {
        internal static bool HasSufficientFunds(this Person person, Money amount)
        {
            return person.Cash.Value >= amount.Value;
        }
    }
}
