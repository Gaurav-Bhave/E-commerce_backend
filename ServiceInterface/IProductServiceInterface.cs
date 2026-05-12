using Practiced_E_commerce.Dto.Products;
using Practiced_E_commerce.Models;

namespace Practiced_E_commerce.ServiceInterface
{
    public interface IProductServiceInterface
    {
        Task<ResponceModel> Createproduct(CreateProductRequestDto createproductdto);

        Task<ResponceModel> GetProducts(Getall_productwithpagination_request_Dto getalldto);

        Task<ResponceModel> Getproductbyid(int id);

        Task<ResponceModel> Deleteproductbyid(int id);

        Task<ResponceModel> Updateproduct(UpdateProductRequestDto updateproductdto);

        Task<ResponceModel> Summery();

    }
}
