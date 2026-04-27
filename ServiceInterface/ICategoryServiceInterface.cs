using Practiced_E_commerce.Dto.Category;
using Practiced_E_commerce.Models;

namespace Practiced_E_commerce.ServiceInterface
{
    public interface ICategoryServiceInterface 
    {
        Task<ResponceModel> GetAllCategory();
        Task<ResponceModel> CreateCategory(CreatecategoryrequestDto createcategorydto);
    }
}
