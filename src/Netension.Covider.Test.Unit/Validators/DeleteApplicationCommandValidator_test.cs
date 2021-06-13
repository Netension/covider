using Netension.Covider.Commands;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Xunit;

namespace Netension.Covider.Test.Unit.Validators
{
    public class DeleteApplicationCommandValidator_test
    {
        private DeleteApplicationCommandValidator CreateSUT()
        {
            return new DeleteApplicationCommandValidator();
        }

        [Theory(DisplayName = "[UNT-DAV][Success] - Delete application")]
        [Trait("", "AM-Application")]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public async Task DeleteApplication_Succes(string name)
        {
            // Arrange
            var sut = CreateSUT();

            // Act
            var result = await sut.ValidateAsync(new DeleteApplicationCommand(name), default);

            // Arrange
            Assert.True(result.IsValid);
        }
    }
}
