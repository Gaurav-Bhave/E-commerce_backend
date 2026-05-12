using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practiced_E_commerce.Dto.Customer.Product;
using Practiced_E_commerce.ServiceInterface.Customer;

namespace Practiced_E_commerce.Controllers.Customer
{
    [Route("api/[controller]")]
    [ApiController]
    public class C_ProductController : ControllerBase
    {
        private readonly I_CProductServiceInterface _CProductServiceInterface;
        public C_ProductController(I_CProductServiceInterface CProductServiceInterface)
        {
            _CProductServiceInterface = CProductServiceInterface;
        }


        [HttpGet("C_AllProduct")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetAllProduct( [FromQuery] C_GetAllProductsRequestDto productrequestdto)
        {
            var result = await _CProductServiceInterface.GetallProducts(productrequestdto);
            return StatusCode(result.StatusCode, result);
        }



        [HttpGet("C_Productdetails")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> C_productdetails(int id)
        { 
            var result = await _CProductServiceInterface.C_productviewdetails(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
