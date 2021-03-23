using AutoFixture;
using GameSource.Models.GameSource;
using GameSource.Tests.Fixtures.Repositories.GameSource;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GameSource.Tests.Repositories
{
    public class DeveloperRepositoryTests : IClassFixture<DeveloperRepositoryFixture>, IDisposable
    {
        DeveloperRepositoryFixture fixture;

        public DeveloperRepositoryTests(DeveloperRepositoryFixture fixture)
        {
            this.fixture = fixture;
        }

        public void Dispose()
        {
            fixture.mockContext.Invocations.Clear();
            fixture.mockDbSet.Invocations.Clear();
        }

        #region GetAll
        [Fact]
        public async Task GetAll_ReturnsListOfDevelopers()
        {
            var developerList = fixture.fixture.Create<IEnumerable<Developer>>();
            var developerDbSet = fixture.SetupMockDbSet(developerList);
            fixture.mockContext.Reset();
            fixture.mockContext.Setup(x => x.Set<Developer>()).Returns(developerDbSet.Object);

            var result = await fixture.developerRepo.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(developerList, result);
        }
        #endregion

        #region GetByID
        [Fact]
        public async Task GetByID_ReturnsDeveloper()
        {
            var developerList = new List<Developer>();
            var developer = new Developer
            {
                ID = 1,
                Name = "BioWare"
            };
            developerList.Add(developer);

            var developerDbSet = fixture.SetupMockDbSet(developerList);
            fixture.mockContext.Reset();
            fixture.mockContext.Setup(x => x.Set<Developer>()).Returns(developerDbSet.Object);

            var result = await fixture.developerRepo.GetByIDAsync(developer.ID);

            Assert.NotNull(result);
            Assert.Equal(developer, result);
        }
        #endregion
    }
}
