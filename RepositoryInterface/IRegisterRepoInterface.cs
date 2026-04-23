using Practiced_E_commerce.Dto.AllUsers;
using Practiced_E_commerce.Dto.Login;
using Practiced_E_commerce.Dto.Register;

namespace Practiced_E_commerce.RepositoryInterface
{
    public interface IRegisterRepoInterface
    {
         Task<RegiterResponceDto> RegisterUser(RegisterDto registerdto);
        Task<LoginResponceDto> LoginUser(LoginUserDto logindto);
        Task<List<AllUsersResponceDto>> AllUsers();
    }
}
