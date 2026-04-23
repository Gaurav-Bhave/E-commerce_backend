using Practiced_E_commerce.Dto.Products;

namespace Practiced_E_commerce.RepositoryInterface
{
    public interface IProductRepoInterface
    {
        Task<List<ProductListResponceDto>> GetAllProduct();
        Task<ProductCreateDto> CreateProduct(ProductCreateDto productcreatedto);
    }
}
