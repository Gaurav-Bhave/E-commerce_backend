using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practiced_E_commerce.Dto.AllUsers;
using Practiced_E_commerce.ServiceInterface;

namespace Practiced_E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersServiceInterface _userservice;
        public UsersController(IUsersServiceInterface userservice)
        {
            _userservice = userservice;
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers( [FromQuery] AllUsersRequestDto alluserrequestdto)
        { 
            var result = await _userservice.GetAllUsers(alluserrequestdto);

            return StatusCode(result.StatusCode, result);
        }

    }
}
