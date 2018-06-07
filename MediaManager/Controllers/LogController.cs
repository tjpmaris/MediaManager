using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaManager.Database;
using MediaManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaManager.Controllers
{
    public interface ILogger
    {
        Task<IActionResult> Log(string email, string message);
    }

    [Produces("application/json")]
    [Route("api/Log")]
    public class LogController : Controller, ILogger
    {
        IDAL dal;

        public LogController(IDAL dal)
        {
            this.dal = dal;
        }

        public async Task<IActionResult> Log(string email, string message)
        {
            if(string.IsNullOrWhiteSpace(email)
                || string.IsNullOrWhiteSpace(message))
            {
                return BadRequest();
            }

            dal.Log(new Log() { Email = email, Message = message });
            return Ok();
        }
    }
}