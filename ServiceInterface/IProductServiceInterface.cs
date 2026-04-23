using Practiced_E_commerce.Dto.Products;
using Practiced_E_commerce.Models;

namespace Practiced_E_commerce.ServiceInterface
{
    public interface IProductServiceInterface
    {
        Task<ResponceModel> GetAllProducts();
        Task<ResponceModel> CreateProduct(ProductCreateDto productcreatedto);
    }
}
