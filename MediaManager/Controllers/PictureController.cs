using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediaManager.Database;
using MediaManager.Models;
using System;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;

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
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var picture = dal.Get<Picture>(id);
                return Ok(picture);
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
                var pictures = dal.GetAllPictures();
                return Ok(pictures);
            }
            catch (MySqlException e)
            {
                return StatusCode(500);
            }
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
            catch (DbUpdateException e)
            {
                return NotFound($"Could not find user with id {create.UserId}");
            }
            catch(MySqlException e)
            {
                return StatusCode(500);
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
                var picture = dal.Delete<Picture>(id);
                return Ok(picture);
            }
            catch (MySqlException e)
            {
                return StatusCode(500);
            }
        }
    }
}