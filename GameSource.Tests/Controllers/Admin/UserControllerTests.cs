using AutoFixture;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSourceUser;
using GameSource.Tests.Fixtures.Controllers.Admin;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameSource.Tests.Controllers.Admin
{
    public class UserControllerTests : IClassFixture<UserControllerFixture>, IDisposable
    {
        UserControllerFixture fixture;

        public UserControllerTests(UserControllerFixture fixture)
        {
            this.fixture = fixture;
        }

        public void Dispose()
        {
            fixture.mockUserRepo.Invocations.Clear();
        }

        #region GetAll
        [Fact]
        public async Task GetAll_SuccessResponse_ReturnsListOfUsers()
        {
            var userList = fixture.fixture.Create<IEnumerable<User>>();

            fixture.mockUserRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(userList);

            var result = await fixture.userController.GetAll();

            fixture.mockUserRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(userList, result.Data);
            Assert.True(result.NumberOfRows > 0);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetAll_SuccessResponse_ReturnsEmptyList()
        {
            fixture.mockUserRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<User>());

            var result = await fixture.userController.GetAll();

            fixture.mockUserRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(Enumerable.Empty<User>(), result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }
        #endregion

        #region GetByID
        [Fact]
        public async Task GetByID_SuccessResponse_ReturnsUser()
        {
            var user = fixture.fixture.Create<User>();

            fixture.mockUserRepo.Setup(x => x.GetByIDAsync(user.Id)).ReturnsAsync(user);

            var result = await fixture.userController.GetByID(user.Id);

            fixture.mockUserRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.IsType<User>(result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetByID_ErrorResponse_WhenIDIs0()
        {
            fixture.mockUserRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((User)null);

            var result = await fixture.userController.GetByID(0);

            fixture.mockUserRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Insert
        [Fact]
        public async Task Insert_SuccessResponse_CreatesUser()
        {
            var user = fixture.fixture.Create<User>();

            fixture.mockUserRepo.Setup(x => x.InsertAsync(user)).ReturnsAsync(1);

            var result = await fixture.userController.Insert(user);

            fixture.mockUserRepo.Verify(x => x.InsertAsync(It.IsAny<User>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Insert_ErrorResponse_WhenUserIsNull()
        {
            fixture.mockUserRepo.Setup(x => x.InsertAsync(null)).ReturnsAsync(0);

            var result = await fixture.userController.Insert(null);

            fixture.mockUserRepo.Verify(x => x.InsertAsync(It.IsAny<User>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_SuccessResponse_UpdatesUser()
        {
            var id = 1;
            var user = new User
            {
                FirstName = "Tom",
                LastName = "Dam"
            };
            var updatedUser = new User
            {
                Id = 1,
                FirstName = "Commander",
                LastName = "Shepard"
            };

            fixture.mockUserRepo.Setup(x => x.GetByIDAsync(id)).ReturnsAsync(updatedUser);
            fixture.mockUserRepo.Setup(x => x.UpdateAsync(updatedUser)).ReturnsAsync(1);

            var result = await fixture.userController.Update(id, user);

            fixture.mockUserRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockUserRepo.Verify(x => x.UpdateAsync(updatedUser), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(updatedUser, result.Data);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_ErrorResponse_WhenIDIs0()
        {
            var user = fixture.fixture.Create<User>();

            fixture.mockUserRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((User)null);

            var result = await fixture.userController.Update(0, user);

            fixture.mockUserRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockUserRepo.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_ErrorResponse_WhenUserIsNull()
        {
            var user = fixture.fixture.Create<User>();

            fixture.mockUserRepo.Setup(x => x.GetByIDAsync(user.Id)).ReturnsAsync(user);
            fixture.mockUserRepo.Setup(x => x.UpdateAsync(null)).ReturnsAsync(0);

            var result = await fixture.userController.Update(user.Id, user);

            fixture.mockUserRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockUserRepo.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_SuccessResponse_DeletesUser()
        {
            var user = fixture.fixture.Create<User>();

            fixture.mockUserRepo.Setup(x => x.GetByIDAsync(user.Id)).ReturnsAsync(user);
            fixture.mockUserRepo.Setup(x => x.DeleteAsync(user)).ReturnsAsync(1);

            var result = await fixture.userController.Delete(user.Id);

            fixture.mockUserRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockUserRepo.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenIDIs0()
        {
            fixture.mockUserRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((User)null);

            var result = await fixture.userController.Delete(0);

            fixture.mockUserRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockUserRepo.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenUserIsNull()
        {
            var user = fixture.fixture.Create<User>();

            fixture.mockUserRepo.Setup(x => x.GetByIDAsync(user.Id)).ReturnsAsync(user);
            fixture.mockUserRepo.Setup(x => x.DeleteAsync(null)).ReturnsAsync(0);

            var result = await fixture.userController.Delete(user.Id);

            fixture.mockUserRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockUserRepo.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion
    }
}
