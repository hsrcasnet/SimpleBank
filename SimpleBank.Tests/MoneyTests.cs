using Xunit;

namespace SimpleBank.Tests
{
    public class MoneyTests
    {
        [Fact]
        public void ShouldCreateMoney()
        {
            // Arrange
            decimal testAmount = 1000;

            // Act
            var testMoney = new Money(testAmount);

            // Assert
            Assert.Equal(testMoney.Value, testAmount);
        }
    }
}