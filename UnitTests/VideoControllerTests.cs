using MediaManager.Models;
using MediaManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MediaManager.Database;

namespace UnitTests
{
    public class VideoControllerTests
    {
        IVideoController controller;

        public VideoControllerTests()
        {
            this.controller = new VideoController(new DAL());
        }

        [Fact]
        public async Task PostTest()
        {
            var actual = await controller.Create(new Video() { Name = "CreateVideo", UserId = 1 });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Video;

            Assert.Equal("CreateVideo", item.Name);
        }

        [Fact]
        public async Task GetByIdTest()
        {
            var actual = await controller.Create(new Video() { Name = "GetIdVideo", UserId = 1 });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Video;

            var actual2 = await controller.GetById(item.Id);
            Assert.IsType<OkObjectResult>(actual2);

            OkObjectResult result2 = actual2 as OkObjectResult;
            var item2 = result2.Value as Video;

            Assert.Equal(item.Id, item2.Id);
            Assert.Equal("GetIdVideo", item2.Name);
        }

        [Fact]
        public async Task GetAllTest()
        {
            await controller.Create(new Video() { Name = "GetAllVideo", UserId = 1 });
            await controller.Create(new Video() { Name = "GetAllVideo", UserId = 1 });
            await controller.Create(new Video() { Name = "GetAllVideo", UserId = 1 });

            var actual = await controller.Get();
            Assert.IsType<OkObjectResult>(actual);
            OkObjectResult result2 = actual as OkObjectResult;
            var items = result2.Value as List<Video>;

            Assert.True(items.Count >= 3);
        }

        [Fact]
        public async Task PutTest()
        {
            var actual = await controller.Create(new Video() { Name = "PutVideo", UserId = 1 });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Video;

            var actual2 = await controller.UpdateEntity(new Video() { Id = item.Id, Name = "Put Video" });
            Assert.IsType<OkObjectResult>(actual2);

            var actual3 = await controller.GetById(item.Id);
            Assert.IsType<OkObjectResult>(actual3);
            OkObjectResult result3 = actual3 as OkObjectResult;
            var item3 = result3.Value as Video;

            Assert.Equal("Put Video", item3.Name);
        }

        [Fact]
        public async Task DeleteTest()
        {
            var actual = await controller.Create(new Video() { Name = "DeleteVideo", UserId = 1 });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Video;

            var actual2 = await controller.Delete(item.Id);
            Assert.IsType<OkObjectResult>(actual2);

            var actual3 = await controller.GetById(item.Id);
            Assert.IsType<OkObjectResult>(actual3);
            OkObjectResult result3 = actual3 as OkObjectResult;
            var item3 = result3.Value as Video;

            Assert.Null(item3);
        }
    }
}
