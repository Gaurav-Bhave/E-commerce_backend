using Practiced_E_commerce.Dto.Customer.Product;
using Practiced_E_commerce.Models;

namespace Practiced_E_commerce.ServiceInterface.Customer
{
    public interface I_CProductServiceInterface
    {
        Task<ResponceModel> GetallProducts(C_GetAllProductsRequestDto c_productrequestdto);

        Task<ResponceModel> C_productviewdetails(int id);
        
    }
}
