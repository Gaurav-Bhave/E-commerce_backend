using Practiced_E_commerce.Dto.Brands;
using Practiced_E_commerce.Models;

namespace Practiced_E_commerce.ServiceInterface
{
    public interface IBrandsServiceInterface
    {
        Task<ResponceModel> GetAllBrands();
        Task<ResponceModel> CreateBrand(Createbrandrequest createbrandrequest);
    }
}
