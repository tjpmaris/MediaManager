using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaManager.ApiModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaManager.Controllers
{
    [Produces("application/json")]
    [Route("api/pictures")]
    public class PictureController : Controller
    {
        private static List<Picture> pictures = new List<Picture>();

        //User
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(pictures.Where(s => s.Id == id).FirstOrDefault());
        }

        //User
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> Get()
        {
            return Ok(pictures);
        }

        //Admin
        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Create([FromBody] Picture picture)
        {
            if(string.IsNullOrWhiteSpace(picture.Name)
                || string.IsNullOrWhiteSpace(picture.Content))
            {
                return BadRequest();
            }

            picture.Id = 1 + (pictures.LastOrDefault()?.Id ?? 0);

            pictures.Add(picture);

            return Created("Created Picture", picture);
        }

        //Admin
        [HttpPut]
        [Route("")]
        public async Task<ActionResult> UpdateEntity([FromBody] Picture picture)
        {
            ActionResult result = BadRequest();

            if(picture.Id is null 
                || string.IsNullOrWhiteSpace(picture.Name)
                || string.IsNullOrWhiteSpace(picture.Content))
            {
                return result;
            }

            var foundPicture = pictures.Where(s => s.Id == picture.Id).FirstOrDefault();

            if(!(foundPicture is null))
            {
                foundPicture.Name = picture.Name;
                foundPicture.Content = picture.Content;

                result = Ok();
            }
            else
            {
                result = NotFound();
            }

            return result;
        }

        //Admin
        [HttpPatch]
        [Route("")]
        public async Task<ActionResult> UpdateProperty([FromBody] Picture picture)
        {
            return await UpdateEntity(picture);
        }

        //Admin
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            pictures.Remove(pictures.Where(s => s.Id == id).FirstOrDefault());
            return Ok();
        }
    }
}