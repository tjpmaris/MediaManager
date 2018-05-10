using MediaManager.ApiModels;
using MediaManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;


namespace UnitTests
{
    public class PictureControllerTests
    {
        PictureController controller;

        [Fact]
        public async Task PostTest()
        {
            controller = new PictureController();

            var actual = await controller.Create(new Picture() { Name = "CreatePicture", Content = "Base64" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Picture;

            Assert.Equal("CreatePicture", item.Name);
        }

        [Fact]
        public async Task GetByIdTest()
        {
            controller = new PictureController();

            var actual = await controller.Create(new Picture() { Name = "GetIdPicture", Content = "Base64" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Picture;

            var actual2 = await controller.GetById((int)item.Id);
            Assert.IsType<OkObjectResult>(actual2);

            OkObjectResult result2 = actual2 as OkObjectResult;
            var item2 = result2.Value as Picture;

            Assert.Equal(item.Id, item2.Id);
            Assert.Equal("GetIdPicture", item2.Name);
            Assert.Equal("Base64", item2.Content);
        }

        [Fact]
        public async Task GetAllTest()
        {
            controller = new PictureController();

            await controller.Create(new Picture() { Name = "GetAllPicture", Content = "Base64" });
            await controller.Create(new Picture() { Name = "GetAllPicture", Content = "Base64" });
            await controller.Create(new Picture() { Name = "GetAllPicture", Content = "Base64" });

            var actual = await controller.Get();
            Assert.IsType<OkObjectResult>(actual);
            OkObjectResult result2 = actual as OkObjectResult;
            var items = result2.Value as List<Picture>;

            Assert.True(items.Count >= 3);
        }

        [Fact]
        public async Task PutTest()
        {
            controller = new PictureController();

            var actual = await controller.Create(new Picture() { Name = "PutPicture", Content = "Base64" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Picture;

            var actual2 = await controller.UpdateEntity(new Picture() { Id = item.Id, Name = "Put Picture", Content = "Base 64" });
            Assert.IsType<OkResult>(actual2);

            var actual3 = await controller.GetById((int)item.Id);
            Assert.IsType<OkObjectResult>(actual3);
            OkObjectResult result3 = actual3 as OkObjectResult;
            var item3 = result3.Value as Picture;

            Assert.Equal("Put Picture", item3.Name);
            Assert.Equal("Base 64", item3.Content);
        }

        [Fact]
        public async Task PatchTest()
        {
            controller = new PictureController();

            var actual = await controller.Create(new Picture() { Name = "PatchSong", Content = "Base64" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Picture;

            var actual2 = await controller.UpdateProperty(new Picture() { Id = item.Id, Name = "Patch Picture", Content = "Base64" });
            Assert.IsType<OkResult>(actual2);

            var actual3 = await controller.GetById((int)item.Id);
            Assert.IsType<OkObjectResult>(actual3);
            OkObjectResult result3 = actual3 as OkObjectResult;
            var item3 = result3.Value as Picture;

            Assert.Equal("Patch Picture", item3.Name);
            Assert.Equal("Base64", item3.Content);
        }

        [Fact]
        public async Task DeleteTest()
        {
            controller = new PictureController();

            var actual = await controller.Create(new Picture() { Name = "DeletePicture", Content = "Base64" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Picture;

            var actual2 = await controller.Delete((int)item.Id);
            Assert.IsType<OkResult>(actual2);

            var actual3 = await controller.GetById((int)item.Id);
            Assert.IsType<OkObjectResult>(actual3);
            OkObjectResult result3 = actual3 as OkObjectResult;
            var item3 = result3.Value as Picture;

            Assert.Null(item3);
        }
    }
}
