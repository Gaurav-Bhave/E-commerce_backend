using System.Data;
using Dapper;
using Practiced_E_commerce.Dto.AllUsers;
using Practiced_E_commerce.RepositoryInterface;

namespace Practiced_E_commerce.Repository
{
    public class UsersRepositrory : IUsersRepoInterface
    {
        private readonly IDbConnection _db;
        public UsersRepositrory(IDbConnection db)
        {
            _db = db;
        }

        public async Task<List<AllUsersResponceDto>> GetAllUsers(AllUsersRequestDto userrequestdto)
        {
            try
            {
                var parameter = new DynamicParameters();

                parameter.Add("@pagenumber", userrequestdto.pagenumber);
                parameter.Add("@pagesize", userrequestdto.pagesize);

                var alldata = await _db.QueryMultipleAsync("GetAllUsers", parameter, commandType: CommandType.StoredProcedure);

                //users
                var users = (await alldata.ReadAsync<AllUsersResponceDto>()).ToList();

                //total count user 
                var total = await alldata.ReadFirstAsync<int>();

                
                foreach (var user in users)
                {
                    user.totalUsers = total;
                }

               return users;

            }

            catch (Exception ex)
            {
                throw new Exception("Error in GetAllUsers method", ex);
            }


        }
    }
}
