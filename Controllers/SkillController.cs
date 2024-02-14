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
    public class SkillController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public SkillController(ApplicationDbContext context){
            _context = context;
        }

        [HttpGet("filterByUserId")]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkillsByUserId([FromQuery] int userid)
        {
            var skills = await _context.Skills.Where(x=>x.UserId == userid).ToListAsync();
            return skills;
        }

        [HttpPost]
        public async Task<ActionResult<Skill>> CreateSkill(Skill skill)
        {
            if(skill.SkillId.HasValue){
                throw new ApiException(400, "Skill Id is auto increament, please do not input skill id");
            }
            var user = await _context.Users.FirstOrDefaultAsync( x=> x.UserId == skill.UserId);
            if(user == null){
                throw new ApiException(400, "This Userid doesn't exst !");   
            }
            if(skill.SkillName == ""){
                throw new ApiException(400, "Please input skill name !");   
            }
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
            return StatusCode(201, skill);
        }
    }
}