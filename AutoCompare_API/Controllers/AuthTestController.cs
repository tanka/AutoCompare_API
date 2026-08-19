

using AutoCompare_API.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/**
* @author Tanka N Sharma
*
*/

namespace AutoCompare_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthTestController
    {
        [HttpGet]
        [Authorize]
        public ActionResult<string> GetSomething()
        {
            return "You are authorized";
        }

        [HttpGet("{someValue:int}")]
        [Authorize(Roles =SD.Role_Admin)]
        public ActionResult<string> GetSomething(int someValue)
        {
            return "You are authorized, with role of Admin";
        }
    }
}
