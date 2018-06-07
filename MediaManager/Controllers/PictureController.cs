using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediaManager.Database;
using MediaManager.Models;
using System;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace MediaManager.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PictureController : Controller
    {
        IDAL dal;

        public PictureController(IDAL dal)
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

            var picture = dal.Get<Picture>(id.ToString());
            return Ok(picture);
        }

        //User
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> Get()
        {
            var pictures = dal.GetAll<Picture>();
            return Ok(pictures);

        }

        //Admin
        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Create([FromBody] Picture create)
        {
            if (string.IsNullOrWhiteSpace(create.Name)
                || string.IsNullOrWhiteSpace(create.UserId))
            {
                return BadRequest();
            }

            var picture = dal.Create(create);
            return Created("Created Picture", picture);
        }

        //Admin
        [HttpPut]
        [Route("")]
        public async Task<ActionResult> UpdateEntity([FromBody] Picture update)
        {
            if (string.IsNullOrWhiteSpace(update.Name)
                || string.IsNullOrWhiteSpace(update.Id))
            {
                return BadRequest();
            }

            var picture = dal.Update(update);
            return Ok(picture);
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

            var picture = dal.Delete<Picture>(id.ToString());
            return Ok(picture);
        }

        [HttpPut]
        [Route("Attach/{id}")]
        public async Task<ActionResult> Attach(Guid id, [FromHeader]string type)
        {
            string fileExtention = DAL.GetFileExtension(type);
            if (string.IsNullOrWhiteSpace(fileExtention)
                || (type != "image/jpeg" && type != "image/png"))
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

            return File(stream, "image/jpg");
        }
    }
}