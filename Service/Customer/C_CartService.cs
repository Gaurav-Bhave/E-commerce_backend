using Practiced_E_commerce.Dto.Customer.Cart;
using Practiced_E_commerce.Models;
using Practiced_E_commerce.RepositoryInterface.Customer;
using Practiced_E_commerce.ServiceInterface.Customer;

namespace Practiced_E_commerce.Service.Customer
{
    public class C_CartService : I_CCartServiceInterface
    {
        private readonly I_CCartRepositoryInterface _CCartRepo;
        public C_CartService(I_CCartRepositoryInterface CCartRepo)
        {
            _CCartRepo = CCartRepo;
        }

        public async Task<ResponceModel> AddToCart(int userId, C_AddToCartRequestDto request)
        {
            var result = await _CCartRepo.AddToCart(userId, request);

            return new ResponceModel
            {
                StatusCode = 200,
                Message = result ? "Added to cart successfully" : "Failed",
                Data = result
            };
        }



        //public async Task<ResponceModel> UpdateCartQuantity(UpdateCartQuantityRequestDto request)
        //{
        //    var result =  _CCartRepo.UpdateCartQuantity(request);

        //    return new ResponceModel
        //    { 
        //        StatusCode = 200,
        //        Message = "increment or decrement succssfully !",
        //        Data = result
        //    };
        //}


        public async Task<ResponceModel> C_UpdateCartQuantity(UpdateCartQuantityRequestDto request, int userId)
        {
            var result = await _CCartRepo.UpdateCartQuantity(request, userId);

            return new ResponceModel
            {
                StatusCode = 200,
                Message = result ?? "increment or decrement successfully !",
                Data = result
            };
        }

      

        public async Task<ResponceModel> RemoveCartItem(int userId, RemoveCartItemDto request)
        {
            var result = await _CCartRepo.RemoveCartItem(userId, request);

            return new ResponceModel
            { 
                StatusCode = 200,
                Message = "Product deleted successfully ",
                Data = result
            };
        }



        public async Task<ResponceModel> GetDataFromCart(int userid)
        {
            var result = await _CCartRepo.GetCartByUser(userid);

            return new ResponceModel
            {
                StatusCode = 200,
                Message = "Get all data from cart table",
                Data = result
            };
        }
    }
}
