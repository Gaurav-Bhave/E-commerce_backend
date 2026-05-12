using System.Data;
using System.Data.Common;
using Dapper;
using Practiced_E_commerce.Dto.Customer.Cart;
using Practiced_E_commerce.RepositoryInterface.Customer;

namespace Practiced_E_commerce.Repository.Customer
{
    public class C_CartRepository : I_CCartRepositoryInterface
    {
        private readonly IDbConnection _db;
        public C_CartRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<bool> AddToCart(int userId, C_AddToCartRequestDto request)
        {
            var param = new DynamicParameters();

            param.Add("@UserId", userId);
            param.Add("@ProductId", request.ProductId);
            param.Add("@Quantity", request.Quantity);

            // OUTPUT PARAMETER
            param.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await _db.ExecuteAsync(
                "C_AddToCart",
                param,
                commandType: CommandType.StoredProcedure
            );

            return param.Get<bool>("@IsSuccess");
        }

     


        ////increment decrement
        //public async Task<string> UpdateCartQuantity(UpdateCartQuantityRequestDto request)
        //{
        //    var parameters = new DynamicParameters();
        //    parameters.Add("@UserId", request.UserId);
        //    parameters.Add("@ProductId", request.ProductId);
        //    parameters.Add("@Action", request.Action);

        //    var result = await _db.QueryFirstOrDefaultAsync<string>(
        //        "sp_UpdateCartQuantity",
        //        parameters,
        //        commandType: CommandType.StoredProcedure
        //    );

        //    return result;
        //}



        //increment decrement 
        public async Task<string> UpdateCartQuantity(UpdateCartQuantityRequestDto request, int userId)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@UserId", userId);
            parameters.Add("@ProductId", request.ProductId);
            parameters.Add("@Action", request.Action);
            parameters.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);

            await _db.ExecuteAsync(
                "sp_UpdateCartQuantity_v1",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return parameters.Get<string>("@Message");
        }


        //remove product from cart
        public async Task<bool> RemoveCartItem(int userId, RemoveCartItemDto request)
        {
            var param = new DynamicParameters();

            param.Add("@UserId", userId);  
            param.Add("@ProductId", request.ProductId); 

            param.Add("@IsSuccess",
                dbType: DbType.Boolean,
                direction: ParameterDirection.Output
            );

            await _db.ExecuteAsync(
                "sp_RemoveCartItem",
                param,
                commandType: CommandType.StoredProcedure
            );

            return param.Get<bool>("@IsSuccess");
        }




        //fetch all data from cart table
        public async Task<List<CartItemResponseDto>> GetCartByUser(int userId)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", userId);

            var result = await _db.QueryAsync<CartItemResponseDto>(
                "sp_GetCartByUser",
                param,
                commandType: CommandType.StoredProcedure
            );

            return result.ToList();
        }
    }
}
