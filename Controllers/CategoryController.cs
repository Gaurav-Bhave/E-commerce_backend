using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practiced_E_commerce.ServiceInterface;

namespace Practiced_E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServiceInterface _catergoryservice;
        public CategoryController(ICategoryServiceInterface catergoryservice) 
        {
            _catergoryservice = catergoryservice;   
        }


        [HttpGet("AllCategory")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllCategory()
        { 
            var result = await _catergoryservice.GetAllCategory();
            return StatusCode(result.StatusCode, result);
        }
    }
}
