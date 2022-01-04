using Xunit;
using AutoFixture;
using Moq;
using TakeOutTheTrash.Api.Controllers;
using TakeOutTheTrash.Api.Repositories;
using TakeOutTheTrash.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace TakeOutTheTrash.Api.UnitTests.Controllers
{
    public class CitiesControllerTests
    {
        private readonly Fixture fixture;

        private readonly Mock<IRepository> repository;

        private readonly CitiesController sut;

        public CitiesControllerTests()
        {
            fixture = new Fixture();

            repository = new Mock<IRepository>();

            sut = new CitiesController(repository.Object);
        }

        [Fact]
        public void Get_ShouldReturnNotFound_IfCityIsNotFound()
        {
            // Arrange
            var id = fixture.Create<int>();

            repository.Setup(r => r.GetCityById(id)).Returns((City)null);

            // Act
            var result = sut.Get(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public void Get_ShouldReturnOKCityResult()
        {
            // Arrange
            var id = fixture.Create<int>();
            var city = fixture.Build<City>().With(c => c.Id, id).Create();

            repository.Setup(r => r.GetCityById(id)).Returns(city);

            // Act
            var result = sut.Get(id) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<City>(result.Value);
            // Set up mock to return more detailed city and assert that city and response are a match.
        }
    }
}

