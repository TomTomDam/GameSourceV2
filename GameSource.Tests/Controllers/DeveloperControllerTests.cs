using AutoFixture;
using GameSource.API.Controllers.GameSource;
using GameSource.Models.GameSource;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using GameSource.Models;
using GameSource.Models.Enums;

namespace GameSource.Tests.Controllers
{
    public class DeveloperControllerTests : IClassFixture<ControllerFixture>
    {
        ControllerFixture fixture;

        public DeveloperControllerTests(ControllerFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task GetAll_ReturnsListOfDevelopers()
        {
            var developerList = fixture.fixture.Create<IEnumerable<Developer>>();

            fixture.mockDeveloperRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(developerList);
            fixture.developerController = new DeveloperController(fixture.mockDeveloperRepo.Object);

            var result = await fixture.developerController.GetAll();

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(result.Data, developerList);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyList()
        {
            fixture.mockDeveloperRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Developer>());
            fixture.developerController = new DeveloperController(fixture.mockDeveloperRepo.Object);

            var result = await fixture.developerController.GetAll();

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(result.Data, Enumerable.Empty<Developer>());
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }
    }
}
