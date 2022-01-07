using Xunit;
using AutoFixture;
using TakeOutTheTrash.Api.Models;
using TakeOutTheTrash.Api.Validators;
using TakeOutTheTrash.Api.UnitTests.Utilities;

namespace TakeOutTheTrash.Api.UnitTests.Validators
{
    public class FeedbackSubmissionValidatorTests
    {
        private readonly Fixture fixture;
        private readonly FeedbackSubmissionValidator sut;
        private readonly TestingUtilities testingUtilities;

        public FeedbackSubmissionValidatorTests()
        {
            fixture = new Fixture();
            sut = new FeedbackSubmissionValidator();
            testingUtilities = new TestingUtilities();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData((string)null)]
        public void Validate_WhenDescriptionIsEmpty_ShouldBeInvalid(string description)
        {
            // Arrange
            var request = fixture.Build<FeedbackSubmission>().With(r => r.Description, description).Create();

            // Act
            var result = sut.Validate(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("'Description' must not be empty.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Validate_WhenDescriptionIsLongerThan500Characters_ShouldBeInvalid()
        {
            // Arrange
            var length = 501;
            var excessivelyLongDescription = testingUtilities.GenerateRandomString(length);
            var request = fixture.Build<FeedbackSubmission>().With(r => r.Description, excessivelyLongDescription).Create();

            // Act
            var result = sut.Validate(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal($"The length of 'Description' must be 500 characters or fewer. You entered {length} characters.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Validate_WhenCityIdIsEmpty_ShouldBeInvalid()
        {
            // Arrange
            var request = fixture.Build<FeedbackSubmission>().Without(r => r.CityId).Create();

            // Act
            var result = sut.Validate(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("'City Id' must not be empty.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Validate_WhenCityIdIsNegativeInt_ShouldBeInvalid()
        {
            // Arrange
            var request = fixture.Build<FeedbackSubmission>().With(r => r.CityId, -1).Create();

            // Act
            var result = sut.Validate(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("'City Id' must be greater than or equal to '0'.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Validate_ShouldReturnMultipleValidationMessages()
        {
            // Arrange
            var request = fixture.Build<FeedbackSubmission>()
                .Without(r => r.CityId)
                .With(r => r.Description, "")
                .Create();

            // Act
            var result = sut.Validate(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("'Description' must not be empty.", result.Errors[0].ErrorMessage);
            Assert.Equal("'City Id' must not be empty.", result.Errors[1].ErrorMessage);
        }

        [Fact]
        public void Validate_ShouldValidate()
        {
            // Arrange
            var acceptablyLongDescription = testingUtilities.GenerateRandomString(500);
            var cityId = fixture.Create<int>();
            var request = fixture.Build<FeedbackSubmission>()
                .With(r => r.CityId, cityId)
                .With(r => r.Description, acceptablyLongDescription)
                .Create();

            // Act
            var result = sut.Validate(request);

            // Assert
            Assert.True(result.IsValid);
        }
    }
}
