using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practiced_E_commerce.Dto.Customer.Cart;
using Practiced_E_commerce.ServiceInterface.Customer;

namespace Practiced_E_commerce.Controllers.Customer
{
    [Route("api/[controller]")]
    [ApiController]
    public class C_CartController : ControllerBase
    {
        private readonly I_CCartServiceInterface _I_CCartServiceInterface;
        public C_CartController(I_CCartServiceInterface I_CCartServiceInterface)
        {
            _I_CCartServiceInterface = I_CCartServiceInterface;
        }


        [HttpPost("AddToCart")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddToCart(C_AddToCartRequestDto request)
        {
            // GET USER ID FROM TOKEN
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = await _I_CCartServiceInterface.AddToCart(userId, request);

            return StatusCode(result.StatusCode, result);
        }


        [HttpPost("update-quantity")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateCartQuantity(UpdateCartQuantityRequestDto request)
        {

            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = await _I_CCartServiceInterface.C_UpdateCartQuantity(request , userId);

            return StatusCode(result.StatusCode, result);
        }


        [HttpPost("remove-item-from-cart")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Removeitemfromcart(RemoveCartItemDto request)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = await _I_CCartServiceInterface.RemoveCartItem(userId, request);

            return StatusCode(result.StatusCode, result);
        }



        [HttpGet("get-cart")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetCartByUser()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = await _I_CCartServiceInterface.GetDataFromCart(userId);

            return StatusCode(result.StatusCode, result);
        }
    }
}
