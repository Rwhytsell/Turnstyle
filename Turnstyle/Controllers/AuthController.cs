using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Turnstyle.Models;
using Turnstyle.Services;

namespace Turnstyle.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        UserService us = new UserService();

        /*
         * This endpoint is used to make sure the auth service is available
         * 
         */
        [HttpGet]
        [Route("auth/ping")]
        public ActionResult Ping()
        {
            return Ok();
        }
        
        /**
         * The login endpoint is used to 
         */
        [HttpPost]
        [Route("auth/login")]
        public ActionResult<string> Login([FromBody] JObject requestData)
        {
            var dbs = new DatabaseService(new triumphContext());
            var (sessionToken, error) = UserService.LoginAccount(requestData, dbs);
            if (sessionToken != null) return Ok(sessionToken);

            Console.Error.WriteLine(error);
            return Unauthorized(error);
        }

        /**
         * The register endpoint is used to create an account to be used throughout the triumph ecosystem
         * Input Required:
         * Username
         * Password
         * Password confirmation
         * Email
         *
         * This endpoint will send the data to the user service and, if successfully registered, will return the users first session token.
         * Otherwise, the endpoint will return the reason wh
         */
        [HttpPost]
        [Route("auth/register")]
        public ActionResult<string> Register([FromBody] JObject requestData)
        {
            var dbs = new DatabaseService(new triumphContext());
            var (account, error) = UserService.RegisterAccount(requestData, dbs);
            if (account != null) return Ok(account);
            
            Console.Error.WriteLine(error);
            return BadRequest(error);
        }

        [HttpGet]
        [Route("auth/check")]
        public ActionResult<Account> Check([FromBody] string token)
        {
            if (SessionService.CheckToken(token))
            {
                return Ok(SessionService.GetValue(token));
            }
            return NotFound();
        }
    }
}