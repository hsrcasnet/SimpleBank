using Xunit;

namespace SimpleBank.Tests
{
    public class AccountTests
    {
        [Fact]
        public void ShouldCreateAccount()
        {
            // Arrange
            const decimal testAmount = 1000;
            const string testUserName = "Test User";

            // Act
            var testUser = new Person(testUserName);
            var testMoney = new Money(testAmount);
            var testAccount = new Account(testMoney, testUser);

            // Assert
            Assert.Equal(testAccount.Amount.Value, testMoney.Value);
        }
    }
}