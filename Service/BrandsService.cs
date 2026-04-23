using Practiced_E_commerce.Models;
using Practiced_E_commerce.RepositoryInterface;
using Practiced_E_commerce.ServiceInterface;

namespace Practiced_E_commerce.Service
{
    public class BrandsService : IBrandsServiceInterface
    {
        private readonly IBrandsRepoInterface _brandsrepo;
        public BrandsService(IBrandsRepoInterface brandsrepo)
        {
            _brandsrepo = brandsrepo;
        }


        public async Task<ResponceModel> GetAllBrands()
        {
            var result = await _brandsrepo.GetAllBrands();

            return new ResponceModel
            {
                StatusCode = 200,
                Message = "Brands list",
                Data = result
            };
        }
    }
}
