using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AuthWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopController : ControllerBase
    {
        private readonly UsersDbContext usersDbContext;

        public DevelopController(UsersDbContext context)
        {
            usersDbContext = context;
        }

        //Filter test
        [Authorize(Policy = "GuestAccess")]
        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }

        //Get user role
        [Authorize(Policy = "AdminAccess")]
        [HttpGet("{UserLogin}")]
        public ActionResult Get(string UserLogin)
        {
            Account item;
            try
            {
                item = usersDbContext.Accounts.Where(a => a.Login == UserLogin).First();
            }
            catch
            {
                return BadRequest();
            }
            if (item != null)
            {
                return Ok(new
                {
                    user_role = item.UserRole
                });
            }
            else
            {
                return NotFound();
            }
        }
    }
}