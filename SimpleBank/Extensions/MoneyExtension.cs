namespace SimpleBank.Extensions
{
    internal static class MoneyExtension
    {
        internal static bool IsPositive(this Money amount)
        {
            return amount.Value > 0;
        }
    }
}