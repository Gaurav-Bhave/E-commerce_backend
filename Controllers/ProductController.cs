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


        [HttpGet("GetProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetProducts([FromQuery] Getall_productwithpagination_request_Dto getproductdto)
        {
            var result = await _productservice.GetProducts(getproductdto);
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet("Getproductbyid/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Getprodductbyid(int id)
        { 
            var result = await _productservice.Getproductbyid(id);
            return StatusCode(result.StatusCode, result);
        }


        [HttpDelete("Deleteproductbyid/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Deleteproductbyid(int id)
        { 
            var result = await _productservice.Deleteproductbyid(id);
            return StatusCode(result.StatusCode, result);
        }



        [HttpPut("Updateproduct")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Updateproduct([FromForm] UpdateProductRequestDto updateproductdto)
        { 
            var result = await _productservice.Updateproduct(updateproductdto);
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet("Summery")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Summery()
        { 
            var result = await _productservice.Summery();
            return StatusCode(result.StatusCode, result);
        }

    }
}
