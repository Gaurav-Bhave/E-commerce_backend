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


        [HttpGet("AllProducts")]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> GetAllProducts()
        { 
            var result = await _productservice.GetAllProducts();
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost("CreateProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct(ProductCreateDto productcreatedto)
        { 
            var result = await _productservice.CreateProduct(productcreatedto);
            return StatusCode(result.StatusCode, result);
        }

    }
}
