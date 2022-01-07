using Xunit;
using AutoFixture;
using Moq;
using TakeOutTheTrash.Api.Controllers;
using TakeOutTheTrash.Api.Repositories;
using TakeOutTheTrash.Api.Models;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;

namespace TakeOutTheTrash.Api.UnitTests.Controllers
{
    public class FeedbackControllerTests
    {
        private readonly Fixture fixture;

        private readonly Mock<IRepository> repository;
        private readonly Mock<IValidator<FeedbackSubmission>> requestValidator;

        private readonly FeedbackController sut;

        public FeedbackControllerTests()
        {
            fixture = new Fixture();

            repository = new Mock<IRepository>();
            requestValidator = new Mock<IValidator<FeedbackSubmission>>();

            sut = new FeedbackController(
                repository.Object,
                requestValidator.Object
                );
        }

        [Fact]
        public void Get_ShouldReturnBadRequest_IfValidationFails()
        {
            // Arrange
            var request = fixture.Create<FeedbackSubmission>();

            const string validationErrorString = "property x is invalid";
            var invalidResult = new ValidationResult(new[] { new ValidationFailure("x", validationErrorString) });
            requestValidator.Setup(a =>
                    a.Validate(request))
                .Returns(invalidResult);

            repository.Setup(r => r.AddFeedbackSubmission(request)).Returns(true);

            var expected = new string[] { validationErrorString };

            // Act
            var result = sut.Post(request) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
            Assert.Equal(expected, result.Value);
        }

        [Fact]
        public void Get_ShouldReturnBadRequest_IfDbInsertFails()
        {
            // Arrange
            var request = fixture.Create<FeedbackSubmission>();

            var validationResult = new ValidationResult();
            requestValidator.Setup(a => a.Validate(request)).Returns(validationResult);

            repository.Setup(r => r.AddFeedbackSubmission(request)).Returns(false);

            // Act
            var result = sut.Post(request) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }

        [Fact]
        public void Get_ShouldReturnOk()
        {
            // Arrange
            var request = fixture.Create<FeedbackSubmission>();

            var validationResult = new ValidationResult();
            requestValidator.Setup(a => a.Validate(request)).Returns(validationResult);

            repository.Setup(r => r.AddFeedbackSubmission(request)).Returns(true);

            // Act
            var result = sut.Post(request) as OkResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsAssignableFrom<OkResult>(result);
        }

        [Fact]
        public void GetErrorStrings()
        {
            // Arrange
            var invalidResult = new ValidationResult(new[] {
                new ValidationFailure("x", "Failure 1."),
                new ValidationFailure("x", "Failure 2."),
                new ValidationFailure("x", "Failure 3.") });
            string[] expected = { "Failure 1.", "Failure 2.", "Failure 3." };

            // Act
            var result = sut.GetErrorStrings(invalidResult);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
