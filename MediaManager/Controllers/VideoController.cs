using System;
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
        public async Task<ActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var video = dal.Get<Video>(id);
                return Ok(video);
            }
            catch (MySqlException e)
            {
                return StatusCode(500);
            }
        }

        //User
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> Get()
        {
            try
            {
                var videos = dal.GetAllVideos();
                return Ok(videos);
            }
            catch (MySqlException e)
            {
                return StatusCode(500);
            }
        }

        //Admin
        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Create([FromBody] Video create)
        {
            if (string.IsNullOrWhiteSpace(create.Name)
                || create.UserId <= 0)
            {
                return BadRequest();
            }

            try
            {
                var video = dal.Create(create);
                return Created("Created Video", video);
            }
            catch (DbUpdateException e)
            {
                return NotFound($"Could not find user with id {create.UserId}");
            }
            catch (MySqlException e)
            {
                return StatusCode(500);
            }
        }

        //Admin
        [HttpPut]
        [Route("")]
        public async Task<ActionResult> UpdateEntity([FromBody] Video update)
        {
            if (string.IsNullOrWhiteSpace(update.Name)
                || update.Id <= 0
                || update.UserId <= 0)
            {
                return BadRequest();
            }

            try
            {
                var video = dal.Update(update);
                return Ok(video);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            catch (DbUpdateException e)
            {
                return NotFound($"Could not find user with id {update.UserId}");
            }
            catch (MySqlException e)
            {
                return StatusCode(500);
            }
        }

        //Admin
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var video = dal.Delete<Video>(id);
                return Ok(video);
            }
            catch (MySqlException e)
            {
                return StatusCode(500);
            }
        }
    }
}