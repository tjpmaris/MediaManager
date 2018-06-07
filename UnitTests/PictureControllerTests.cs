using MediaManager.Models;
using MediaManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using MediaManager.Database;
using System;

namespace UnitTests
{
    public class PictureControllerTests
    {
        PictureController controller;
        public PictureControllerTests()
        {
            this.controller = new PictureController(new DAL());
        }

        [Fact]
        public async Task PostTest()
        {
            var actual = await controller.Create(new Picture() { Name = "CreatePicture", UserId = "1" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Picture;

            Assert.Equal("CreatePicture", item.Name);
        }

        [Fact]
        public async Task GetByIdTest()
        {
            var actual = await controller.Create(new Picture() { Name = "GetIdPicture", UserId = "1" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Picture;

            var actual2 = await controller.GetById(Guid.Parse(item.Id));
            Assert.IsType<OkObjectResult>(actual2);

            OkObjectResult result2 = actual2 as OkObjectResult;
            var item2 = result2.Value as Picture;

            Assert.Equal(item.Id, item2.Id);
            Assert.Equal("GetIdPicture", item2.Name);
        }

        [Fact]
        public async Task GetAllTest()
        {
            await controller.Create(new Picture() { Name = "GetAllPicture", UserId = "1" });
            await controller.Create(new Picture() { Name = "GetAllPicture", UserId = "1" });
            await controller.Create(new Picture() { Name = "GetAllPicture", UserId = "1" });

            var actual = await controller.Get();
            Assert.IsType<OkObjectResult>(actual);
            OkObjectResult result2 = actual as OkObjectResult;
            var items = result2.Value as List<Picture>;

            Assert.True(items.Count >= 3);
        }

        [Fact]
        public async Task PutTest()
        {
            var actual = await controller.Create(new Picture() { Name = "PutPicture", UserId = "1" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Picture;

            var actual2 = await controller.UpdateEntity(new Picture() { Id = item.Id, Name = "Put Picture" });
            Assert.IsType<OkObjectResult>(actual2);

            var actual3 = await controller.GetById(Guid.Parse(item.Id));
            Assert.IsType<OkObjectResult>(actual3);
            OkObjectResult result3 = actual3 as OkObjectResult;
            var item3 = result3.Value as Picture;

            Assert.Equal("Put Picture", item3.Name);
        }

        [Fact]
        public async Task DeleteTest()
        {
            var actual = await controller.Create(new Picture() { Name = "DeletePicture", UserId = "1" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Picture;

            var actual2 = await controller.Delete(Guid.Parse(item.Id));
            Assert.IsType<OkObjectResult>(actual2);

            var actual3 = await controller.GetById(Guid.Parse(item.Id));
            Assert.IsType<OkObjectResult>(actual3);
            OkObjectResult result3 = actual3 as OkObjectResult;
            var item3 = result3.Value as Picture;

            Assert.Null(item3);
        }
    }
}
