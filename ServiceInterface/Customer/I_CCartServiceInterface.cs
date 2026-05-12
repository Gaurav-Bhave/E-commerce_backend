using Practiced_E_commerce.Dto.Customer.Cart;
using Practiced_E_commerce.Models;

namespace Practiced_E_commerce.ServiceInterface.Customer
{
    public interface I_CCartServiceInterface
    {
        Task<ResponceModel> AddToCart(int userId, C_AddToCartRequestDto request);

        Task<ResponceModel> C_UpdateCartQuantity(UpdateCartQuantityRequestDto request, int userId);

        Task<ResponceModel> RemoveCartItem(int userId, RemoveCartItemDto request);

        Task<ResponceModel> GetDataFromCart(int userid);
        

    }
}
