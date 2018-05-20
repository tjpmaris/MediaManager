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
        public async Task<ActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var user = dal.Get<User>(id);
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
                || (int)create.Role < 0 
                || (int)create.Role > 1)
            {
                return BadRequest();
            }

            try
            {
                var user = dal.Create(create);
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
                || update.Id <= 0 
                || (int)update.Role < 0
                || (int)update.Role > 1)
            {
                return BadRequest();
            }

            try
            {
                var user = dal.Update(update);
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
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var user = dal.Delete<User>(id);
                return Ok(user);
            }
            catch (MySqlException e)
            {
                return StatusCode(500);
            }
        }
    }
}