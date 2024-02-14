using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using webapi.Model;
using webapi.CustomException;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExceptionController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetException()
        {
            throw new ApiException(500, "This is intension error !");
            return Ok("Demo return message");
        }

        [HttpGet("Unhandle")]
        public ActionResult<IEnumerable<User>> GetUnhandledException()
        {
            throw new Exception("Unhandled errpr !!");
            return Ok("Demo return message");
        }
    }
}