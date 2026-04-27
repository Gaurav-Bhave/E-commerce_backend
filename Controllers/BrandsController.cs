using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practiced_E_commerce.Dto.Brands;
using Practiced_E_commerce.ServiceInterface;

namespace Practiced_E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandsServiceInterface _brandsservice;
        public BrandsController(IBrandsServiceInterface brandsservice)
        {
            _brandsservice = brandsservice;
        }

        [HttpGet("AllBrands")]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> GetAllBrands()
        { 
            var result = await _brandsservice.GetAllBrands();
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("CreateBrand")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBrand(Createbrandrequest createbrandrequest)
        { 
            var result = await _brandsservice.CreateBrand(createbrandrequest);
            return StatusCode(result.StatusCode, result);
        }
    }
}
