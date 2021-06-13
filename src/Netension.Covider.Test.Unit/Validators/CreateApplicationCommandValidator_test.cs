using Fare;
using FluentValidation;
using Netension.Covider.Commands;
using System.Threading.Tasks;
using Xunit;

namespace Netension.Covider.Test.Unit.Validators
{
    public class CreateApplicationCommandValidator_test
    {
        private CreateApplicationCommandValidator CreateSUT()
        {
            return new CreateApplicationCommandValidator();
        }

        [Fact(DisplayName = "[UNT-CAV001][Success] - Validate name")]
        [Trait("", "AM-Application")]
        public async Task ValidateName_Success()
        {
            // Arrange
            var sut = CreateSUT();
            
            // Act
            var result = await sut.ValidateAsync(new CreateApplicationCommand(new Xeger("^[a-z][a-z0-9_$()+/-]*$").Generate()), default);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "[UNT-CAV002][Failure] - Use invalid name")]
        [Trait("", "AM-Application")]
        public async Task ValidateName_InvalidName_Failure()
        {
            // Arrange
            var sut = CreateSUT();

            // Act
            var result = await sut.ValidateAsync(new CreateApplicationCommand(new Xeger("[^a-z][^a-z[0-9_$()+/-][^*$]").Generate()), default);

            // Arrange
            Assert.False(result.IsValid);
        }

        [Theory(DisplayName = "[UNT-CAV003][Failure] - Empty application name")]
        [Trait("", "AM-Application")]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public async Task ValidateName_EmptyName_Failure(string name)
        {
            // Arrange
            var sut = CreateSUT();

            // Act
            var result = await sut.ValidateAsync(new CreateApplicationCommand(name), default);

            // Arrange
            Assert.False(result.IsValid);
        }
    }
}
