using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace SimpleBank.Tests
{
    public class BankTests
    {
        private const string TestName = "UBS";
        private const string TestPerson1Name = "Thomas Galliker";
        private const string TestPerson2Name = "Peter Lustig";
        private const decimal TestMoneyAccount1Amount = 1000;
        private const decimal TestMoneyAccount2Amount = 1000;

        [Fact]
        public void ShouldCreateBank()
        {
            // Act
            var bank = new Bank(TestName);

            // Assert
            bank.Name.Should().Be(TestName);
        }

        [Fact]
        public void ShouldCreateAccount()
        {
            // Arrange
            const decimal moneyPersonAmount = 1600;

            var bank = new Bank(TestName);
            var customer = new Person(TestPerson1Name)
            {
                Cash = new Money(moneyPersonAmount)
            };
            var initialDeposit = new Money(1000);

            // Act
            var account = bank.CreateAccount(customer, initialDeposit);

            // Assert
            account.Should().NotBeNull();
            account.Amount.Value.Should().Be(initialDeposit.Value);

            var accountsForCustomer = bank.GetAccounts(customer).ToList();
            accountsForCustomer.Should().HaveCount(1);
            accountsForCustomer.Sum(a => a.Amount.Value).Should().Be(initialDeposit.Value);

            customer.Cash.Value.Should().Be(moneyPersonAmount - initialDeposit.Value);
        }

        [Fact]
        public void ShouldCreateAccount_ThrowsExceptionIfNegativeValue()
        {
            // Arrange
            const decimal moneyPersonAmount = 1600;
            const decimal moneyAccountAmount = -1000;

            var bank = new Bank(TestName);
            var moneyPerson = new Money(moneyPersonAmount);
            var person = new Person(TestPerson1Name)
            {
                Cash = moneyPerson
            };
            var moneyAccount = new Money(moneyAccountAmount);

            // Act
            Action action = () => bank.CreateAccount(person, moneyAccount);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ShouldCreateAccount_ThrowsExceptionIfInsufficientFunds()
        {
            // Arrange
            const decimal moneyPersonAmount = 800;

            var bank = new Bank(TestName);
            var moneyPerson = new Money(moneyPersonAmount);
            var person = new Person(TestPerson1Name)
            {
                Cash = moneyPerson
            };
            var moneyAccount = new Money(TestMoneyAccount1Amount);

            // Act
            Action action = () => bank.CreateAccount(person, moneyAccount);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ShouldAccountWithdraw()
        {
            // Arrange
            const decimal moneyPersonAmount = 1600;
            const decimal testMoneyWithdrawAmount = 300;

            var bank = new Bank(TestName);
            var moneyPerson = new Money(moneyPersonAmount);
            var person = new Person(TestPerson1Name)
            {
                Cash = moneyPerson
            };
            var initialDeposit = new Money(TestMoneyAccount1Amount);
            var withdrawMoney = new Money(testMoneyWithdrawAmount);
            var account = bank.CreateAccount(person, initialDeposit);

            // Act
            bank.Withdraw(account.Id, withdrawMoney);

            // Assert
            account.Amount.Value.Should().Be(initialDeposit.Value - withdrawMoney.Value);
        }

        [Fact]
        public void ShouldAccountWithdraw_ThrowsExceptionIfNegativeValue()
        {
            // Arrange
            const decimal moneyPersonAmount = 1600;
            decimal testMoneyWithdrawAmount = -300;

            var bank = new Bank(TestName);
            var moneyPerson = new Money(moneyPersonAmount);
            var person = new Person(TestPerson1Name)
            {
                Cash = moneyPerson
            };
            var initialDeposit = new Money(TestMoneyAccount1Amount);
            var testMoneyWithdraw = new Money(testMoneyWithdrawAmount);
            var account = bank.CreateAccount(person, initialDeposit);

            // Act
            Action action = () => bank.Withdraw(account.Id, testMoneyWithdraw);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ShouldAccountWithdraw_ThrowsExceptionIfInsufficientFunds()
        {
            // Arrange
            const decimal moneyPersonAmount = 1600;
            const decimal testMoneyWithdrawAmount = 3000;

            var bank = new Bank(TestName);
            var moneyPerson = new Money(moneyPersonAmount);
            var person = new Person(TestPerson1Name)
            {
                Cash = moneyPerson
            };
            var initialDeposit = new Money(TestMoneyAccount1Amount);
            var testMoneyWithdraw = new Money(testMoneyWithdrawAmount);
            var account = bank.CreateAccount(person, initialDeposit);

            // Act
            Action action = () => bank.Withdraw(account.Id, testMoneyWithdraw);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ShouldAccountDeposit()
        {
            // Arrange
            const decimal moneyPersonAmount = 1600;
            const decimal testMoneyDepositAmount = 300;

            var bank = new Bank(TestName);
            var moneyPerson = new Money(moneyPersonAmount);
            var person = new Person(TestPerson1Name)
            {
                Cash = moneyPerson
            };
            var initialDeposit = new Money(TestMoneyAccount1Amount);
            var account = bank.CreateAccount(person, initialDeposit);

            var depositMoney = new Money(testMoneyDepositAmount);

            // Act
            bank.Deposit(account.Id, depositMoney);

            // Assert
            account.Amount.Value.Should().Be(initialDeposit.Value + depositMoney.Value);
        }

        [Fact]
        public void ShouldAccountDeposit_ThrowsExceptionIfNegativeValue()
        {
            // Arrange
            const decimal moneyPersonAmount = 1600;
            const decimal testMoneyDepositAmount = -300;

            var bank = new Bank(TestName);
            var moneyPerson = new Money(moneyPersonAmount);
            var person = new Person(TestPerson1Name)
            {
                Cash = moneyPerson
            };
            var initialDeposit = new Money(TestMoneyAccount1Amount);
            var account = bank.CreateAccount(person, initialDeposit);

            var depositMoney = new Money(testMoneyDepositAmount);

            // Act
            Action action = () => bank.Deposit(account.Id, depositMoney);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ShouldAccountTransfer()
        {
            // Arrange
            const decimal moneyPerson1Amount = 1600;
            const decimal moneyPerson2Amount = 1600;
            const decimal testMoneyTransferAmount = 300;

            var bank = new Bank(TestName);

            var moneyPerson1 = new Money(moneyPerson1Amount);
            var person1 = new Person(TestPerson1Name)
            {
                Cash = moneyPerson1
            };

            var moneyPerson2 = new Money(moneyPerson2Amount);
            var person2 = new Person(TestPerson2Name)
            {
                Cash = moneyPerson2
            };

            var initialDeposit1 = new Money(TestMoneyAccount1Amount);
            var initialDeposit2 = new Money(TestMoneyAccount2Amount);
            var transferMoney = new Money(testMoneyTransferAmount);

            var account1 = bank.CreateAccount(person1, initialDeposit1);
            var account2 = bank.CreateAccount(person2, initialDeposit2);

            // Act
            bank.Transfer(account1.Id, account2.Id, transferMoney);

            // Assert
            account1.Amount.Value.Should().Be(initialDeposit1.Value - transferMoney.Value);
            account2.Amount.Value.Should().Be(initialDeposit2.Value + transferMoney.Value);
        }

        [Fact]
        public void ShouldAccountTransfer_ThrowsExceptionIfNegativeValue()
        {
            // Arrange
            const decimal moneyPerson1Amount = 1600;
            const decimal moneyPerson2Amount = 1600;
            const decimal testMoneyTransferAmount = -200;

            var bank = new Bank(TestName);

            var moneyPerson1 = new Money(moneyPerson1Amount);
            var person1 = new Person(TestPerson1Name)
            {
                Cash = moneyPerson1
            };

            var moneyPerson2 = new Money(moneyPerson2Amount);
            var person2 = new Person(TestPerson2Name)
            {
                Cash = moneyPerson2
            };

            var initialDeposit1 = new Money(TestMoneyAccount1Amount);
            var initialDeposit2 = new Money(TestMoneyAccount2Amount);
            var testMoneyTransfer = new Money(testMoneyTransferAmount);

            var account1 = bank.CreateAccount(person1, initialDeposit1);
            var account2 = bank.CreateAccount(person2, initialDeposit2);

            // Act
            Action action = () => bank.Transfer(account1.Id, account2.Id, testMoneyTransfer);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ShouldAccountTransfer_ThrowsExceptionIfInsufficientFunds()
        {
            // Arrange
            const decimal moneyPerson1Amount = 1600;
            const decimal moneyPerson2Amount = 1600;
            const decimal testMoneyTransferAmount = 2000;

            var bank = new Bank(TestName);

            var moneyPerson1 = new Money(moneyPerson1Amount);
            var person1 = new Person(TestPerson1Name)
            {
                Cash = moneyPerson1
            };

            var moneyPerson2 = new Money(moneyPerson2Amount);
            var person2 = new Person(TestPerson2Name)
            {
                Cash = moneyPerson2
            };

            var initialDeposit1 = new Money(TestMoneyAccount1Amount);
            var initialDeposit2 = new Money(TestMoneyAccount2Amount);
            var testMoneyTransfer = new Money(testMoneyTransferAmount);

            var account1 = bank.CreateAccount(person1, initialDeposit1);
            var account2 = bank.CreateAccount(person2, initialDeposit2);

            // Act
            Action action = () => bank.Transfer(account1.Id, account2.Id, testMoneyTransfer);

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}