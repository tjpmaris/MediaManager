using System;
using System.IO;
using System.Threading.Tasks;
using MediaManager.Database;
using MediaManager.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace MediaManager.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        IDAL dal;

        public UserController(IDAL dal)
        {
            this.dal = dal;
        }

        //User
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest();
            }

            try
            {
                var user = dal.GetUser(email);
                return Ok(user);
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
                var users = dal.GetAllUsers();
                return Ok(users);
            }
            catch (MySqlException e)
            {
                return StatusCode(500);
            }
        }

        //Admin
        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Create([FromBody] User create)
        {
            if (string.IsNullOrWhiteSpace(create.Name)
                || string.IsNullOrWhiteSpace(create.Role))
            {
                return BadRequest();
            }

            try
            {
                var user = dal.CreateUser(create);
                return Created("Created User", user);
            }
            catch (MySqlException e)
            {
                return StatusCode(500);
            }
        }

        //Admin
        [HttpPut]
        [Route("")]
        public async Task<ActionResult> UpdateEntity([FromBody] User update)
        {
            if (string.IsNullOrWhiteSpace(update.Name)
                || string.IsNullOrWhiteSpace(update.Role))
            {
                return BadRequest();
            }

            try
            {
                var user = dal.UpdateUser(update);
                return Ok(user);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            catch (MySqlException e)
            {
                return StatusCode(500);
            }
        }

        //Admin
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest();
            }

            try
            {
                var user = dal.DeleteUser(email);
                return Ok(user);
            }
            catch (MySqlException e)
            {
                return StatusCode(500);
            }
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

            return File(stream, "video/mp4a");
        }
    }
}