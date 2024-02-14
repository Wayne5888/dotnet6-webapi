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

namespace webapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserRoleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserRoleController(ApplicationDbContext context){
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAllUserRoles()
        {
            var UserRole = await _context.UserRoles.Include(x=>x.User).Include(x=>x.Role).Select(
                x=> new {
                    UserId = x.UserId,
                    UserName = x.User.Name,
                    RoleId = x.RoleId,
                    RoleName = x.Role.Name
                }
            ).ToListAsync();
            return Ok(UserRole);
        }

        [HttpPost]
        public async Task<ActionResult<UserRole>> AddUserRole(UserRole ur)
        {
            var userrole = await _context.UserRoles.FirstOrDefaultAsync(x=>x.UserId == ur.UserId && x.RoleId == ur.RoleId);
            if(userrole != null){
                throw new ApiException(400, "This User Role already exist !");
            }
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.UserId == ur.UserId);
            if(user == null){
                throw new ApiException(400, "This User doesn't exist !");
            }
            var role = await _context.Roles.FirstOrDefaultAsync(x=>x.RoleId == ur.RoleId);
            if(role == null){
                throw new ApiException(400, "This Role doesn't exist !");
            }
            _context.UserRoles.Add(ur);
            await _context.SaveChangesAsync();
            return Ok(ur);
        }

        
    }
}