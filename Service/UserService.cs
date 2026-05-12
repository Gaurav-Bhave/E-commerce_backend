using Practiced_E_commerce.Dto.AllUsers;
using Practiced_E_commerce.Models;
using Practiced_E_commerce.RepositoryInterface;
using Practiced_E_commerce.ServiceInterface;
using WireMock.Admin.Mappings;

namespace Practiced_E_commerce.Service
{
    public class UserService : IUsersServiceInterface
    {
        private readonly IUsersRepoInterface _userrepo;

        public UserService(IUsersRepoInterface userrepo)
        {
            _userrepo = userrepo;
        }

        // get all users
        public async Task<ResponceModel> GetAllUsers(AllUsersRequestDto alluserdto)
        {
            var result = await _userrepo.GetAllUsers(alluserdto);

            return new ResponceModel
            { 
                StatusCode = 200,
                Message = "Fetch all users successfully !",
                Data = result
            };
        }

        

    }
}