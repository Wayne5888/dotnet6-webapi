using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using webapi.Model;
using webapi.CustomException;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        public LoginController(){


        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetJWTToken()
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this_is_my_custom_Secret_key_for_authentication"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Issuser",
                audience: "Audience",
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }
}