using Practiced_E_commerce.Models;

namespace Practiced_E_commerce.ServiceInterface
{
    public interface ICategoryServiceInterface 
    {
        Task<ResponceModel> GetAllCategory();
    }
}
