﻿using System;
using System.Collections;
using System.Collections.Generic;
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
            return Accepted();
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

        [HttpGet("{token}")]
        [Route("auth/check")]
        public ActionResult<Account> Check([FromQuery] string token)
        {
            var ( account, error ) = SessionService.GetValue(token);
            if (string.IsNullOrEmpty(error))
            {

                List<string> roles = new List<string>();
                foreach (var r in account.UserroleUser)
                {
                    roles.Add(r.Id.ToString());
                }

                var response = new Dictionary<string, object>
                {
                    { "ID", account.Id.ToString() },
                    { "Roles", roles }
                };
                return Ok( response );
            }
            return NotFound();
        }
    }
}