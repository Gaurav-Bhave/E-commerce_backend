using Practiced_E_commerce.Dto.Products;

namespace Practiced_E_commerce.RepositoryInterface
{
    public interface IProductRepoInterface
    {
        Task<CreateProductResponseDto> Createproduct(CreateProductRequestDto createproductdto);
        Task<Getall_productswithpagination_response_dto> GetAllProducts(Getall_productwithpagination_request_Dto getallproductdto);

    }
}
