using FluentAssertions;
using Xunit;

namespace SimpleBank.Tests
{
    public class PersonTests
    {
        [Fact]
        public void ShouldCreatePerson()
        {
            // Arrange
            var testUser = new Person("Test User");

            // Act
            testUser.Name.Should().Be("Test User");
        }
    }
}