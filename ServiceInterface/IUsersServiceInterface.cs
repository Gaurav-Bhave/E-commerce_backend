using Practiced_E_commerce.Dto.AllUsers;
using Practiced_E_commerce.Models;
using WireMock.Admin.Mappings;

namespace Practiced_E_commerce.ServiceInterface
{
    public interface IUsersServiceInterface
    {
        Task<ResponceModel> GetAllUsers(AllUsersRequestDto alluserdto);

    }
}
