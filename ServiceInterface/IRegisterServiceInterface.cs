using Practiced_E_commerce.Dto.Login;
using Practiced_E_commerce.Dto.Register;
using Practiced_E_commerce.Models;
using WireMock.Admin.Mappings;

namespace Practiced_E_commerce.ServiceInterface
{
    public interface IRegisterServiceInterface
    {
        Task<ResponceModel> RegisterUser(RegisterDto registerdto);
        Task<ResponceModel> LoginUser(LoginUserDto logindto);
        Task<ResponceModel> AllUsers();
        
    }
}
