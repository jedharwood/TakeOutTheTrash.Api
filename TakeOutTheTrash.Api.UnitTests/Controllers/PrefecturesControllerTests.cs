using Xunit;
using AutoFixture;
using Moq;
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

            repository.Setup(r => r.GetAllPrefectures()).Returns(new List<Prefecture>());

            // Act
            var result = sut.Get();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public void Get_ShouldReturnOKPrefecturesResult_IfNoIdProvided()
        {
            // Arrange
            var prefecturesList = new List<Prefecture> { fixture.Create<Prefecture>() };

            repository.Setup(r => r.GetAllPrefectures()).Returns(prefecturesList);

            // Act
            var result = sut.Get() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<List<Prefecture>>(result.Value);
            Assert.IsAssignableFrom<OkObjectResult>(result);
            // Set up mock to return more detailed list and assert that list and response are a match.
        }

        [Fact]
        public void Get_ShouldReturnNotFound_WhenIdProvided_IfCitiesCollectionIsEmpty()
        {
            // Arrange
            var id = fixture.Create<int>();
            var citiesList = new List<City>();

            repository.Setup(r => r.GetAllCitiesByPrefectureId(id)).Returns(citiesList);

            // Act
            var result = sut.Get(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public void Get_ShouldReturnOKCitiesResult_WhenIdProvided()
        {
            // Arrange
            var id = fixture.Create<int>();
            var cityPrefecture = fixture.Build<CityPrefecture>().With(cp => cp.Id, id).Create();
            var citiesList = new List<City> { fixture.Build<City>().With(c => c.Prefecture, cityPrefecture).Create() };

            repository.Setup(r => r.GetAllCitiesByPrefectureId(id)).Returns(citiesList);

            // Act
            var result = sut.Get(id) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<List<City>>(result.Value);
            Assert.IsAssignableFrom<OkObjectResult>(result);
            // Set up mock to return more detailed list and assert that list and response are a match.
        }
    }
}

