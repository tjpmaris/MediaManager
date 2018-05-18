using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediaManager.Database;
using MediaManager.Models;
using System;

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
        public async Task<ActionResult> GetById(int id)
        {
            var picture = dal.GetPictureById(id);

            return Ok(picture);
        }

        //User
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> Get()
        {
            var pictures = dal.GetAllPictures();

            return Ok(pictures);
        }

        //Admin
        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Create([FromBody] Picture create)
        {
            if (string.IsNullOrWhiteSpace(create.Name)
                || create.UserId <= 0)
            {
                return BadRequest();
            }

            try
            {
                var picture = dal.Create(create);
                return Created("Created Picture", picture);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        //Admin
        [HttpPut]
        [Route("")]
        public async Task<ActionResult> UpdateEntity([FromBody] Picture update)
        {
            if (string.IsNullOrWhiteSpace(update.Name)
                || update.Id <= 0
                || update.UserId <= 0)
            {
                return BadRequest();
            }

            try
            {
                var picture = dal.Update(update);
                return Ok(picture);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        ////Admin
        //[HttpPatch]
        //[Route("")]
        //public async Task<ActionResult> UpdateProperty([FromBody] Picture picture)
        //{
        //    return await UpdateEntity(picture);
        //}

        //Admin
        //[HttpDelete]
        //[Route("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    pictures.Remove(pictures.Where(s => s.Id == id).FirstOrDefault());
        //    return Ok();
        //}
    }
}