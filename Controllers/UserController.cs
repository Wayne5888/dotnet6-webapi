using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using webapi.Model;
using webapi.CustomException;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace webapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserController> _logger;

        public UserController(ApplicationDbContext context, ILogger<UserController> logger){
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            _logger.LogInformation($"Received GET request GetUserById : {id}");
            var user = await _context.Users.FindAsync(id);            
            if (user == null)
            {
                throw new ApiException(404, "This user is not found !");
            }
            _logger.LogInformation($"Returning GET response for ID: {id}");
            return Ok(user);
        }

        [HttpGet("filterByName")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByName([FromQuery] string name){
            if (string.IsNullOrEmpty(name))
            {
                throw new ApiException(400, "Name parameter is required.");
            }

            var users = await _context.Users.Where(x => x.Name == name).ToListAsync();
            return users;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            if(user.UserId.HasValue){
                throw new ApiException(400, "User Id is auto increament, please do not input user id");
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return StatusCode(201, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User updateUser){
            User user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new ApiException(404, "This user is not found !");
            }
            user.Name = updateUser.Name;
            user.Email = updateUser.Email;
            user.NickName = updateUser.NickName;
            await _context.SaveChangesAsync();
            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id){
            User user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new ApiException(404, "This user is not found !");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return StatusCode(200);
        }
    }
}