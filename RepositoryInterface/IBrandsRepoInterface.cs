using Practiced_E_commerce.Dto.Brands;

namespace Practiced_E_commerce.RepositoryInterface
{
    public interface IBrandsRepoInterface
    {
        Task<List<BrandsDto>> GetAllBrands();
    }
}
