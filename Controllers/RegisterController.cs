using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practiced_E_commerce.Dto.Login;
using Practiced_E_commerce.Dto.Register;
using Practiced_E_commerce.ServiceInterface;

namespace Practiced_E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterServiceInterface _registerservice;
        public RegisterController(IRegisterServiceInterface registerservice)
        {
            _registerservice = registerservice;
        }


        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterDto registerdto)
        { 
            var result = await _registerservice.RegisterUser(registerdto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser(LoginUserDto logindto)
        { 
            var result = await _registerservice.LoginUser(logindto);
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet("AllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        { 
            var result = await _registerservice.AllUsers();
            return StatusCode(result.StatusCode, result);
        }


    }
}
