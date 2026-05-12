using Practiced_E_commerce.Dto.Customer.Cart;

namespace Practiced_E_commerce.RepositoryInterface.Customer
{
    public interface I_CCartRepositoryInterface
    {
        Task<bool> AddToCart(int userId, C_AddToCartRequestDto request);

        Task<string> UpdateCartQuantity(UpdateCartQuantityRequestDto request, int userId);

        Task<bool> RemoveCartItem(int userId, RemoveCartItemDto request);

        Task<List<CartItemResponseDto>> GetCartByUser(int userId);

    }
}
