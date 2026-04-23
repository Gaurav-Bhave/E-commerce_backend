using Practiced_E_commerce.Models;

namespace Practiced_E_commerce.ServiceInterface
{
    public interface IBrandsServiceInterface
    {
        Task<ResponceModel> GetAllBrands();
    }
}
