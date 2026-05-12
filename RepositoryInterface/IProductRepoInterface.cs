using Practiced_E_commerce.Dto.Products;
using Practiced_E_commerce.Dto.Summery;

namespace Practiced_E_commerce.RepositoryInterface
{
    public interface IProductRepoInterface
    {
        Task<CreateProductResponseDto> Createproduct(CreateProductRequestDto createproductdto);

        Task<List<Getall_productswithpagination_response_dto>> GetProducts(Getall_productwithpagination_request_Dto getalldto);

        Task<ProductDetailsResponseDto> Getproductbyid(int id);
        Task<DeleteProductResponse_Dto> Deleteproductbyid(int id);

        Task<UpdateProductResponseDto> Updateproduct(UpdateProductRequestDto requestDto);

        Task<Overall_Data_Response> Summery();




    }
}
