//using MediaManager.Controllers;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using MediaManager.Models;
//using Xunit;
//using MediaManager.Database;

//namespace UnitTests
//{
//    public class UserControllerTests
//    {
//        UserController controller;
//        public UserControllerTests()
//        {
//            this.controller = new UserController(new DAL());
//        }

//        [Fact]
//        public async Task PostTest()
//        {
//            var actual = await controller.Create(new User() { UserName = "CreateUser", Role = "User" });
//            Assert.IsType<CreatedResult>(actual);
//            CreatedResult result = actual as CreatedResult;
//            var item = result.Value as User;

//            Assert.Equal("CreateUser", item.UserName);
//        }

//        [Fact]
//        public async Task GetByIdTest()
//        {
//            var actual = await controller.Create(new User() { UserName = "GetIdUser", Role = "User" });
//            Assert.IsType<CreatedResult>(actual);
//            CreatedResult result = actual as CreatedResult;
//            var item = result.Value as User;

//            var actual2 = await controller.GetById(item.Id);
//            Assert.IsType<OkObjectResult>(actual2);

//            OkObjectResult result2 = actual2 as OkObjectResult;
//            var item2 = result2.Value as User;

//            Assert.Equal(item.Id, item2.Id);
//            Assert.Equal("GetIdUser", item2.UserName);
//        }

//        [Fact]
//        public async Task GetAllTest()
//        {
//            await controller.Create(new User() { UserName = "GetAllUser", Role = "User" });
//            await controller.Create(new User() { UserName = "GetAllUser", Role = "User" });
//            await controller.Create(new User() { UserName = "GetAllUser", Role = "User" });

//            var actual = await controller.Get();
//            Assert.IsType<OkObjectResult>(actual);
//            OkObjectResult result2 = actual as OkObjectResult;
//            var items = result2.Value as List<User>;

//            Assert.True(items.Count >= 3);
//        }

//        [Fact]
//        public async Task PutTest()
//        {
//            var actual = await controller.Create(new User() { UserName = "PutUser", Role = "User" });
//            Assert.IsType<CreatedResult>(actual);
//            CreatedResult result = actual as CreatedResult;
//            var item = result.Value as User;

//            var actual2 = await controller.UpdateEntity(new User() { Id = item.Id, UserName = "Put User", Role = "User" });
//            Assert.IsType<OkObjectResult>(actual2);

//            var actual3 = await controller.GetById(item.Id);
//            Assert.IsType<OkObjectResult>(actual3);
//            OkObjectResult result3 = actual3 as OkObjectResult;
//            var item3 = result3.Value as User;

//            Assert.Equal("Put User", item3.UserName);
//        }

//        [Fact]
//        public async Task DeleteTest()
//        {
//            var actual = await controller.Create(new User() { UserName = "DeleteUser", Role = "User" });
//            Assert.IsType<CreatedResult>(actual);
//            CreatedResult result = actual as CreatedResult;
//            var item = result.Value as User;

//            var actual2 = await controller.Delete(item.Id);
//            Assert.IsType<OkObjectResult>(actual2);

//            var actual3 = await controller.GetById(item.Id);
//            Assert.IsType<OkObjectResult>(actual3);
//            OkObjectResult result3 = actual3 as OkObjectResult;
//            var item3 = result3.Value as User;

//            Assert.Null(item3);
//        }
//    }
//}
