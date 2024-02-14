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
    public class RoleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoleController(ApplicationDbContext context){
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetAllRoles()
        {
            return await _context.Roles.Select(x=> x).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRoleById(int id)
        {
            var Role = await _context.Roles.FindAsync(id);            
            if (Role == null)
            {
                throw new ApiException(404, "This role is not found !");
            }
            return Ok(Role);
        }

        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole(Role role)
        {
            if(role.RoleId.HasValue){
                throw new ApiException(400, "role Id is auto increament, please do not input role id");
            }
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return StatusCode(201, role);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, Role updatedRole){
            Role role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                throw new ApiException(400, "This role is not found !");
            }
            role.Name = updatedRole.Name;
            await _context.SaveChangesAsync();
            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id){
            Role role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                throw new ApiException(400, "This role is not found !");
            }
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return StatusCode(200);
        }

    }
}