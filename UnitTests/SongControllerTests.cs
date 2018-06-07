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
    public class SongControllerTests
    {
        SongController controller;
        public SongControllerTests()
        {
            this.controller = new SongController(new DAL());
        }

        [Fact]
        public async Task PostTest()
        {
            var actual = await controller.Create(new Song() { Name = "CreateSong", UserId = "1" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Song;

            Assert.Equal("CreateSong", item.Name);
        }

        [Fact]
        public async Task GetByIdTest()
        {
            var actual = await controller.Create(new Song() { Name = "GetIdSong", UserId = "1" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Song;

            var actual2 = await controller.GetById(Guid.Parse(item.Id));
            Assert.IsType<OkObjectResult>(actual2);

            OkObjectResult result2 = actual2 as OkObjectResult;
            var item2 = result2.Value as Song;

            Assert.Equal(item.Id, item2.Id);
            Assert.Equal("GetIdSong", item2.Name);
        }

        [Fact]
        public async Task GetAllTest()
        {
            await controller.Create(new Song() { Name = "GetAllSong", UserId = "1" });
            await controller.Create(new Song() { Name = "GetAllSong", UserId = "1" });
            await controller.Create(new Song() { Name = "GetAllSong", UserId = "1" });

            var actual = await controller.Get();
            Assert.IsType<OkObjectResult>(actual);
            OkObjectResult result2 = actual as OkObjectResult;
            var items = result2.Value as List<Song>;

            Assert.True(items.Count >= 3);
        }

        [Fact]
        public async Task PutTest()
        {
            var actual = await controller.Create(new Song() { Name = "PutSong", UserId = "1" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Song;

            var actual2 = await controller.UpdateEntity(new Song() { Id = item.Id, Name = "Put Song" });
            Assert.IsType<OkObjectResult>(actual2);

            var actual3 = await controller.GetById(Guid.Parse(item.Id));
            Assert.IsType<OkObjectResult>(actual3);
            OkObjectResult result3 = actual3 as OkObjectResult;
            var item3 = result3.Value as Song;

            Assert.Equal("Put Song", item3.Name);
        }

        [Fact]
        public async Task DeleteTest()
        {
            var actual = await controller.Create(new Song() { Name = "DeleteSong", UserId = "1" });
            Assert.IsType<CreatedResult>(actual);
            CreatedResult result = actual as CreatedResult;
            var item = result.Value as Song;

            var actual2 = await controller.Delete(Guid.Parse(item.Id));
            Assert.IsType<OkObjectResult>(actual2);

            var actual3 = await controller.GetById(Guid.Parse(item.Id));
            Assert.IsType<OkObjectResult>(actual3);
            OkObjectResult result3 = actual3 as OkObjectResult;
            var item3 = result3.Value as Song;

            Assert.Null(item3);
        }
    }
}
