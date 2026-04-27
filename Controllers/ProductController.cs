using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practiced_E_commerce.Dto.Products;
using Practiced_E_commerce.ServiceInterface;

namespace Practiced_E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServiceInterface _productservice;
        public ProductController(IProductServiceInterface productservice)
        {
            _productservice = productservice;
        }

        [HttpPost("CreateProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequestDto createproductdto)
        { 
            var result = await _productservice.Createproduct(createproductdto);
            return StatusCode(result.StatusCode, result);
        }

    }
}
