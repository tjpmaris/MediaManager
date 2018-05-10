using MediaManager.ApiModels;
using MediaManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class VideoControllerTests
    {
        VideoController controller;

        [Fact]
        public async Task PostTest()
        {
            controller = new VideoController();

            var actual = await controller.Create(new Video() { Name = "CreateVideo", Content = "Base64" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Video;

            Assert.Equal("CreateVideo", item.Name);
        }

        [Fact]
        public async Task GetByIdTest()
        {
            controller = new VideoController();

            var actual = await controller.Create(new Video() { Name = "GetIdVideo", Content = "Base64" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Video;

            var actual2 = await controller.GetById((int)item.Id);
            Assert.IsType<OkObjectResult>(actual2);

            OkObjectResult result2 = actual2 as OkObjectResult;
            var item2 = result2.Value as Video;

            Assert.Equal(item.Id, item2.Id);
            Assert.Equal("GetIdVideo", item2.Name);
            Assert.Equal("Base64", item2.Content);
        }

        [Fact]
        public async Task GetAllTest()
        {
            controller = new VideoController();

            await controller.Create(new Video() { Name = "GetAllVideo", Content = "Base64" });
            await controller.Create(new Video() { Name = "GetAllVideo", Content = "Base64" });
            await controller.Create(new Video() { Name = "GetAllVideo", Content = "Base64" });

            var actual = await controller.Get();
            Assert.IsType<OkObjectResult>(actual);
            OkObjectResult result2 = actual as OkObjectResult;
            var items = result2.Value as List<Video>;

            Assert.True(items.Count >= 3);
        }

        [Fact]
        public async Task PutTest()
        {
            controller = new VideoController();

            var actual = await controller.Create(new Video() { Name = "PutVideo", Content = "Base64" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Video;

            var actual2 = await controller.UpdateEntity(new Video() { Id = item.Id, Name = "Put Video", Content = "Base 64" });
            Assert.IsType<OkResult>(actual2);

            var actual3 = await controller.GetById((int)item.Id);
            Assert.IsType<OkObjectResult>(actual3);
            OkObjectResult result3 = actual3 as OkObjectResult;
            var item3 = result3.Value as Video;

            Assert.Equal("Put Video", item3.Name);
            Assert.Equal("Base 64", item3.Content);
        }

        [Fact]
        public async Task PatchTest()
        {
            controller = new VideoController();

            var actual = await controller.Create(new Video() { Name = "PatchVideo", Content = "Base64" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Video;

            var actual2 = await controller.UpdateProperty(new Video() { Id = item.Id, Name = "Patch Video", Content = "Base64" });
            Assert.IsType<OkResult>(actual2);

            var actual3 = await controller.GetById((int)item.Id);
            Assert.IsType<OkObjectResult>(actual3);
            OkObjectResult result3 = actual3 as OkObjectResult;
            var item3 = result3.Value as Video;

            Assert.Equal("Patch Video", item3.Name);
            Assert.Equal("Base64", item3.Content);
        }

        [Fact]
        public async Task DeleteTest()
        {
            controller = new VideoController();

            var actual = await controller.Create(new Video() { Name = "Delete", Content = "Base64" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Video;

            var actual2 = await controller.Delete((int)item.Id);
            Assert.IsType<OkResult>(actual2);

            var actual3 = await controller.GetById((int)item.Id);
            Assert.IsType<OkObjectResult>(actual3);
            OkObjectResult result3 = actual3 as OkObjectResult;
            var item3 = result3.Value as Video;

            Assert.Null(item3);
        }
    }
}
