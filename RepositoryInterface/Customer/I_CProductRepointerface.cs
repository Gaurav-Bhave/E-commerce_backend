using Practiced_E_commerce.Dto.Customer.Product;
using Practiced_E_commerce.Dto.Products;

namespace Practiced_E_commerce.RepositoryInterface.Customer
{
    public interface I_CProductRepointerface
    {
        Task<PagedProductResponseDto> GetlAllProducts(C_GetAllProductsRequestDto getallproductrequestdto);

        Task<ProductDetailsResponseDto> C_Productdetailsbyid(int id);
    }
}
