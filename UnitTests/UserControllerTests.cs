using MediaManager.Controllers;
using MediaManager.ApiModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaManager.Models;
using Xunit;

namespace UnitTests
{
    public class UserControllerTests
    {
        UserController controller;

        [Fact]
        public async Task PostTest()
        {
            controller = new UserController();

            var actual = await controller.Create(new UserCreateModify() { Username = "CreateUser", Password = "password", Role = Role.User });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as UserCreateModify;

            Assert.Equal("CreateUser", item.Username);
        }

        [Fact]
        public async Task GetByIdTest()
        {
            controller = new UserController();

            var actual = await controller.Create(new UserCreateModify() { Username = "GetIdUser", Password = "password", Role = Role.User });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as UserCreateModify;

            var actual2 = await controller.GetById((int)item.Id);
            Assert.IsType<OkObjectResult>(actual2);

            OkObjectResult result2 = actual2 as OkObjectResult;
            var item2 = result2.Value as FlatUser;

            Assert.Equal(item.Id, item2.Id);
            Assert.Equal("GetIdUser", item2.Username);
            Assert.Equal(Role.User, item2.Role);
        }

        [Fact]
        public async Task GetAllTest()
        {
            controller = new UserController();

            await controller.Create(new UserCreateModify() { Username = "GetAllUser", Password = "password", Role = Role.User });
            await controller.Create(new UserCreateModify() { Username = "GetAllUser", Password = "password", Role = Role.User });
            await controller.Create(new UserCreateModify() { Username = "GetAllUser", Password = "password", Role = Role.User });

            var actual = await controller.Get();
            Assert.IsType<OkObjectResult>(actual);
            OkObjectResult result2 = actual as OkObjectResult;
            var items = result2.Value as List<FlatUser>;

            Assert.True(items.Count >= 3);
        }

        [Fact]
        public async Task PutTest()
        {
            controller = new UserController();

            var actual = await controller.Create(new UserCreateModify() { Username = "PutUser", Password = "password", Role = Role.User });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as UserCreateModify;

            var actual2 = await controller.UpdateEntity(new UserCreateModify() { Id = item.Id, Username = "Put User", Password = "pass", Role = Role.Admin });
            Assert.IsType<OkResult>(actual2);

            var actual3 = await controller.GetById((int)item.Id);
            Assert.IsType<OkObjectResult>(actual3);
            OkObjectResult result3 = actual3 as OkObjectResult;
            var item3 = result3.Value as FlatUser;

            Assert.Equal("Put User", item3.Username);
            Assert.Equal(Role.Admin, item3.Role);
        }

        [Fact]
        public async Task PatchTest()
        {
            controller = new UserController();

            var actual = await controller.Create(new UserCreateModify() { Username = "PatchUser", Password = "password", Role = Role.User });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as UserCreateModify;

            var actual2 = await controller.UpdateProperty(new UserCreateModify() { Id = item.Id, Username = "Patch User", Password = "password", Role = Role.User });
            Assert.IsType<OkResult>(actual2);

            var actual3 = await controller.GetById((int)item.Id);
            Assert.IsType<OkObjectResult>(actual3);
            OkObjectResult result3 = actual3 as OkObjectResult;
            var item3 = result3.Value as FlatUser;

            Assert.Equal("Patch User", item3.Username);
            Assert.Equal(Role.User, item3.Role);
        }

        [Fact]
        public async Task DeleteTest()
        {
            controller = new UserController();

            var actual = await controller.Create(new UserCreateModify() { Username = "DeleteUser", Password = "password", Role = Role.User });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as UserCreateModify;

            var actual2 = await controller.Delete((int)item.Id);
            Assert.IsType<OkResult>(actual2);

            var actual3 = await controller.GetById((int)item.Id);
            Assert.IsType<OkObjectResult>(actual3);
            OkObjectResult result3 = actual3 as OkObjectResult;
            var item3 = result3.Value as FlatUser;

            Assert.Null(item3);
        }
    }
}
