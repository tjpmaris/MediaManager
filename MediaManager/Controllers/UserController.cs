using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaManager.ApiModels;
using MediaManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaManager.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UserController : Controller
    {
        private static List<User> users = new List<User>();

        //User
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(Flatten(users.Where(s => s.Id == id).FirstOrDefault()));
        }

        //User
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> Get()
        {
            return Ok(Flatten(users));
        }

        //Admin
        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Create([FromBody] UserCreateModify user)
        {
            if (string.IsNullOrWhiteSpace(user.Username)
                || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest();
            }

            user.Id = 1 + (users.LastOrDefault()?.Id ?? 0);

            users.Add(new User() { Id = (int)user.Id, Username = user.Username, Password = user.Password, Role = user.Role });

            return Created("Created User", user);
        }

        //Admin
        [HttpPut]
        [Route("")]
        public async Task<ActionResult> UpdateEntity([FromBody] UserCreateModify user)
        {
            if (user.Id is null
                || string.IsNullOrWhiteSpace(user.Username)
                || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest();
            }

            var foundUser = users.Where(s => s.Id == user.Id).FirstOrDefault();

            if (!(foundUser is null))
            {
                foundUser.Username = user.Username;
                foundUser.Password = user.Password;
                foundUser.Role = user.Role;

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        //Admin
        [HttpPatch]
        [Route("")]
        public async Task<ActionResult> UpdateProperty([FromBody] UserCreateModify user)
        {
            return await UpdateEntity(user);
        }

        //Admin
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            users.Remove(users.Where(s => s.Id == id).FirstOrDefault());
            return Ok();
        }

        private FlatUser Flatten(User user)
        {
            FlatUser result = null;

            if (user != null)
            {
                result = new FlatUser(user);
            }

            return result;
        }

        private List<FlatUser> Flatten(List<User> users)
        {
            List<FlatUser> results = new List<FlatUser>();

            users?.ForEach(s => results.Add(Flatten(s)));

            return results;
        }
    }
}