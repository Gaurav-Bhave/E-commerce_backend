using Practiced_E_commerce.Dto.Customer.Product;
using Practiced_E_commerce.Models;
using Practiced_E_commerce.RepositoryInterface.Customer;
using Practiced_E_commerce.ServiceInterface.Customer;

namespace Practiced_E_commerce.Service.Customer
{
    public class C_ProductService : I_CProductServiceInterface
    {
        private readonly I_CProductRepointerface _CProductRepointerface;
        public C_ProductService(I_CProductRepointerface CProductRepointerface)
        {
            _CProductRepointerface = CProductRepointerface;
        }

      

        public async Task<ResponceModel> GetallProducts(C_GetAllProductsRequestDto c_productrequestdto)
        {
            var result = await _CProductRepointerface.GetlAllProducts(c_productrequestdto);

            return new ResponceModel
            {
                Message = "All product fetched succcessfully !",
                StatusCode = 200,
                Data = result
            };
        }

        //customer side product view details by product id
        public async Task<ResponceModel> C_productviewdetails(int id)
        {
            var result = await _CProductRepointerface.C_Productdetailsbyid(id);

            return new ResponceModel
            {
                Message = "Product view details successfully fetched !",
                StatusCode=200,
                Data= result
            };
        }
    }
}
