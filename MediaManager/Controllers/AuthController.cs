using MediaManager.Database;
using MediaManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MediaManager.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        IDAL dal;
        ILogger logger;
        public AuthController(IDAL dal, ILogger logger)
        {
            this.dal = dal;
            this.logger = logger;
        }

        ////Reset Password
        //[HttpPost]
        //[Route("password/reset")]
        //public async Task<ActionResult> ResetPassword([FromForm]string email)
        //{
        //    User user = dal.GetUser(email);

        //    if (user != null)
        //    {
        //        string newPassword = Guid.NewGuid().ToString();
        //        //send email with new password
        //        SmtpClient client = new SmtpClient("tjmparis@gmail.com");
        //        MailAddress from = new MailAddress("tjpmaris@gmail.com");
        //        MailAddress to = new MailAddress(email);
        //        MailMessage message = new MailMessage(from, to);
        //        message.Body = $"Your password was set to {newPassword}. Please change your password after logging in to your account.";
        //        message.Subject = "Password Reset";
        //        client.Send(message);
        //        return Ok();
        //    }

        //    return NotFound(email);
        //}

        //Change Password
        [HttpPut]
        [Route("Password/Change")]
        public async Task<ActionResult> ChangePassword([FromForm]string email, [FromForm]string oldPassword,
                                                        [FromForm]string newPassword, [FromForm]string authToken)
        {
            //get user
            User user = dal.GetUser(email);
            //check passwords
            if (user.AuthToken != authToken || user.Password != oldPassword)
            {
                return Unauthorized();
            }

            //update user
            user.Password = newPassword;
            dal.UpdateUser(user);

            await logger.Log(user.Email, "Changed their password.");

            return Ok();
        }

        //Login
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromForm]string email, [FromForm]string password)
        {
            //get user
            User user = dal.GetUser(email);
            //check password
            if (user.Password == password)
            {
                Guid guid = Guid.NewGuid();

                //update token
                user.AuthToken = guid.ToString();
                dal.UpdateUser(user);

                //return token
                return Ok(guid.ToString());
            }

            return Ok();
        }

        //Register
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register([FromForm]string email, [FromForm]string username, [FromForm]string password)
        {
            //check if email exists
            //check if username exists
            var users = dal.GetAllUsers();
            if (users.Where(s => s.Name == username || s.Email == email).Any())
            {
                return StatusCode(409);
            }

            //create user
            User user = new User();
            user.Name = username;
            user.Email = email;
            user.Role = "User";

            //add password
            user.Password = password;

            UserController controller = new UserController(dal);
            var response = await controller.Create(user);
            await logger.Log(user.Email, "User was created.");

            return response;
        }
    }
}
