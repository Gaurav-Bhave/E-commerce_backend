using Practiced_E_commerce.Dto.Login;
using Practiced_E_commerce.Dto.Register;
using Practiced_E_commerce.Models;
using Practiced_E_commerce.RepositoryInterface;
using Practiced_E_commerce.ServiceInterface;
using WireMock.Admin.Mappings;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Practiced_E_commerce.Service
{
    public class RegisterService : IRegisterServiceInterface
    {
        private readonly IRegisterRepoInterface _repoInterface;
        public RegisterService(IRegisterRepoInterface repoInterface)
        {
            _repoInterface = repoInterface;
        }


        public async Task<ResponceModel> RegisterUser(RegisterDto registerdto)
        {
            try
            {
                var result = await _repoInterface.RegisterUser(registerdto);

                return new ResponceModel 
                {
                    StatusCode = 200,
                    Message = "User Register Successfully !",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponceModel
                {
                    StatusCode = 409,
                    Message = ex.Message,
                    Data = null
                };
            }
        }


        public async Task<ResponceModel> LoginUser(LoginUserDto logindto)
        {
            try
            {
                var result = await _repoInterface.LoginUser(logindto);

                return new ResponceModel
                {
                    StatusCode = 200,
                    Message = "Login Successfully !",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponceModel
                {
                    StatusCode = 401,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponceModel> AllUsers()
        {
            var result = await _repoInterface.AllUsers();

            return new ResponceModel
            {
                StatusCode = 200,
                Message = "List of All Users",
                Data = result
            };
        }
    }
}
