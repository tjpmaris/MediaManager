using System;
using System.IO;
using System.Threading.Tasks;
using MediaManager.Database;
using MediaManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace MediaManager.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class VideoController : Controller
    {
        IDAL dal;

        public VideoController(IDAL dal)
        {
            this.dal = dal;
        }

        //User
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var video = dal.Get<Video>(id.ToString());
            return Ok(video);
        }

        //User
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> Get()
        {
            return Ok(dal.GetAll<Video>());
        }

        //Admin
        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Create([FromBody] Video create)
        {
            if (string.IsNullOrWhiteSpace(create.Name)
                || string.IsNullOrWhiteSpace(create.UserId))
            {
                return BadRequest();
            }

            var video = dal.Create(create);
            return Created("Created Video", video);
        }

        //Admin
        [HttpPut]
        [Route("")]
        public async Task<ActionResult> UpdateEntity([FromBody] Video update)
        {
            if (string.IsNullOrWhiteSpace(update.Name)
                || string.IsNullOrWhiteSpace(update.Id))
            {
                return BadRequest();
            }

            var video = dal.Update(update);
            return Ok(video);
        }

        //Admin
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var video = dal.Delete<Video>(id.ToString());
            return Ok(video);
        }

        [HttpPost]
        [Route("Attachment/{id}")]
        public async Task<ActionResult> PostAttachment(Guid id, [FromHeader]string type)
        {
            string fileExtention = DAL.GetFileExtension(type);
            if (string.IsNullOrWhiteSpace(fileExtention)
                || type != "video/mp4"
                || id == Guid.Empty)
            {
                return BadRequest();
            }

            string filename = id.ToString() + fileExtention;
            FileStream file = new FileStream(filename, FileMode.Create);

            int bite = Request.Body.ReadByte();
            while (bite >= 0)
            {
                file.WriteByte((byte)bite);
                bite = Request.Body.ReadByte();
            }

            file.Position = 0;
            dal.Attach(id.ToString(), file, type);

            file.Close();

            System.IO.File.Delete(filename);

            return Ok();
        }

        [HttpGet]
        [Route("Attachment/{id}")]
        public async Task<ActionResult> GetAttachment(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            Stream stream = dal.GetAttachment(id.ToString());

            return File(stream, "video/mp4");
        }
    }
}