using Xunit;
using AutoFixture;
using Moq;
using TakeOutTheTrash.Api.Responses;
using TakeOutTheTrash.Api.Controllers;
using TakeOutTheTrash.Api.Repositories;
using TakeOutTheTrash.Api.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace TakeOutTheTrash.Api.UnitTests.Controllers
{
    public class PrefecturesControllerTests
    {
        private readonly Fixture fixture;

        private readonly Mock<IRepository> repository;

        private readonly PrefecturesController sut;

        public PrefecturesControllerTests()
        {
            fixture = new Fixture();

            repository = new Mock<IRepository>();

            sut = new PrefecturesController(repository.Object);
        }

        [Fact]
        public void Get_ShouldReturnNotFound_IfPrefecturesCollectionEmpty()
        {
            // Arrange
            var prefecturesList = new List<Prefecture>();

            repository.Setup(r => r.GetAllPrefectures()).Returns(prefecturesList);

            // Act
            var result = sut.Get();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public void Get_ShouldReturnOKPrefecturesResult()
        {
            // Arrange
            var prefecturesList = new List<Prefecture>();
            prefecturesList.Add(fixture.Create<Prefecture>());

            repository.Setup(r => r.GetAllPrefectures()).Returns(prefecturesList);

            // Act
            var result = sut.Get() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<PrefecturesResponse>(result.Value);
            // Set up mock to return more detailed list and assert that list and response are a match.
        }
    }
}

