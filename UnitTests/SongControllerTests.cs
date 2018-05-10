using MediaManager.ApiModels;
using MediaManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class SongControllerTests
    {
        SongController controller;

        [Fact]
        public async Task PostTest()
        {
            controller = new SongController();

            var actual = await controller.Create(new Song() { Name = "CreateSong", Content = "Base64" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Song;

            Assert.Equal("CreateSong", item.Name);
        }

        [Fact]
        public async Task GetByIdTest()
        {
            controller = new SongController();

            var actual = await controller.Create(new Song() { Name = "GetIdSong", Content = "Base64" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Song;

            var actual2 = await controller.GetById((int)item.Id);
            Assert.IsType<OkObjectResult>(actual2);

            OkObjectResult result2 = actual2 as OkObjectResult;
            var item2 = result2.Value as Song;

            Assert.Equal(item.Id, item2.Id);
            Assert.Equal("GetIdSong", item2.Name);
            Assert.Equal("Base64", item2.Content);
        }

        [Fact]
        public async Task GetAllTest()
        {
            controller = new SongController();

            await controller.Create(new Song() { Name = "GetAllSong", Content = "Base64" });
            await controller.Create(new Song() { Name = "GetAllSong", Content = "Base64" });
            await controller.Create(new Song() { Name = "GetAllSong", Content = "Base64" });

            var actual = await controller.Get();
            Assert.IsType<OkObjectResult>(actual);
            OkObjectResult result2 = actual as OkObjectResult;
            var items = result2.Value as List<Song>;

            Assert.True(items.Count >= 3);
        }

        [Fact]
        public async Task PutTest()
        {
            controller = new SongController();

            var actual = await controller.Create(new Song() { Name = "PutSong", Content = "Base64" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Song;

            var actual2 = await controller.UpdateEntity(new Song() { Id = item.Id, Name = "Put Song", Content = "Base 64" });
            Assert.IsType<OkResult>(actual2);

            var actual3 = await controller.GetById((int)item.Id);
            Assert.IsType<OkObjectResult>(actual3);
            OkObjectResult result3 = actual3 as OkObjectResult;
            var item3 = result3.Value as Song;

            Assert.Equal("Put Song", item3.Name);
            Assert.Equal("Base 64", item3.Content);
        }

        [Fact]
        public async Task PatchTest()
        {
            controller = new SongController();

            var actual = await controller.Create(new Song() { Name = "PatchSong", Content = "Base64" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Song;

            var actual2 = await controller.UpdateProperty(new Song() { Id = item.Id, Name = "Patch Song", Content = "Base64" });
            Assert.IsType<OkResult>(actual2);

            var actual3 = await controller.GetById((int)item.Id);
            Assert.IsType<OkObjectResult>(actual3);
            OkObjectResult result3 = actual3 as OkObjectResult;
            var item3 = result3.Value as Song;

            Assert.Equal("Patch Song", item3.Name);
            Assert.Equal("Base64", item3.Content);
        }

        [Fact]
        public async Task DeleteTest()
        {
            controller = new SongController();

            var actual = await controller.Create(new Song() { Name = "DeleteSong", Content = "Base64" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Song;

            var actual2 = await controller.Delete((int)item.Id);
            Assert.IsType<OkResult>(actual2);

            var actual3 = await controller.GetById((int)item.Id);
            Assert.IsType<OkObjectResult>(actual3);
            OkObjectResult result3 = actual3 as OkObjectResult;
            var item3 = result3.Value as Song;

            Assert.Null(item3);
        }
    }
}
