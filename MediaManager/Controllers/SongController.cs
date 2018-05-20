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
        public async Task<ActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var song = dal.Get<Song>(id);
                return Ok(song);
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
                var songs = dal.GetAllSongs();
                return Ok(songs);
            }
            catch (MySqlException e)
            {
                return StatusCode(500);
            }
        }

        //Admin
        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Create([FromBody] Song create)
        {
            if (string.IsNullOrWhiteSpace(create.Name)
                || create.UserId <= 0)
            {
                return BadRequest();
            }

            try
            {
                var song = dal.Create(create);
                return Created("Created Song", song);
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
        public async Task<ActionResult> UpdateEntity([FromBody] Song update)
        {
            if (string.IsNullOrWhiteSpace(update.Name)
                || update.Id <= 0
                || update.UserId <= 0)
            {
                return BadRequest();
            }

            try
            {
                var song = dal.Update(update);
                return Ok(song);
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
                var song = dal.Delete<Song>(id);
                return Ok(song);
            }
            catch (MySqlException e)
            {
                return StatusCode(500);
            }
        }
    }
}