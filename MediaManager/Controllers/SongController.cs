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
    public class SongController : Controller
    {
        IDAL dal;

        public SongController(IDAL dal)
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

            var song = dal.Get<Song>(id.ToString());
            return Ok(song);
        }

        //User
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> Get()
        {
            return Ok(dal.GetAll<Song>());
        }

        //Admin
        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Create([FromBody] Song create)
        {
            if (string.IsNullOrWhiteSpace(create.Name)
                || string.IsNullOrWhiteSpace(create.UserId))
            {
                return BadRequest();
            }

            var song = dal.Create(create);
            return Created("Created Song", song);
        }

        //Admin
        [HttpPut]
        [Route("")]
        public async Task<ActionResult> UpdateEntity([FromBody] Song update)
        {
            if (string.IsNullOrWhiteSpace(update.Name)
                || string.IsNullOrWhiteSpace(update.Id))
            {
                return BadRequest();
            }

            var song = dal.Update(update);
            return Ok(song);
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

            var song = dal.Delete<Song>(id.ToString());
            return Ok(song);
        }

        [HttpPut]
        [Route("Attach/{id}")]
        public async Task<ActionResult> Attach(Guid id, [FromHeader]string type)
        {
            string fileExtention = DAL.GetFileExtension(type);
            if (string.IsNullOrWhiteSpace(fileExtention)
                || type != "audio/mpeg")
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
    }
}