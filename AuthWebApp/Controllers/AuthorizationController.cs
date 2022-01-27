using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using AuthCommon;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using AuthWebApp.Models;

namespace AuthWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IOptions<AuthOptions> authOptions;
        private readonly UsersDbContext usersDbContext;

        public AuthorizationController(IOptions<AuthOptions> authOptions, UsersDbContext context)
        {
            this.authOptions = authOptions;
            usersDbContext = context;
        }


        [HttpPost]
        public ActionResult Post([FromBody] LoginRequest request)
        {
            var user = AuthenticateUser(request.Login, request.Password); 
            if (user != null)
            {
                var token = GenerateJWT(user);

                return Ok(new UserModel 
                { 
                    UserRole = user.UserRole,
                    Token = token
                });
            }

            return Unauthorized();
        }

        private Account AuthenticateUser(string login, string passord)
        {
            return usersDbContext.Accounts.SingleOrDefault(u => u.Login == login && u.Password == passord);
        }

        private string GenerateJWT(Account user)
        {
            var authParams = authOptions.Value;
            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            string userRole = user.UserRole == UserRoles.Guest ? "Guest" : "Admin";

            var claimsList = new List<Claim>()
            {
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Login),
                new Claim(ClaimTypes.Role, userRole)
            };
            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(issuer: authParams.Issuer,
                audience: authParams.Audience,
                notBefore: now,
                claims: claimsList,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}