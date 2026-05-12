using Practiced_E_commerce.Dto.AllUsers;

namespace Practiced_E_commerce.RepositoryInterface
{
    public interface IUsersRepoInterface
    {
        Task<List<AllUsersResponceDto>> GetAllUsers(AllUsersRequestDto userrequestdto);
    }
}
