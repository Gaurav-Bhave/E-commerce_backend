using System.Data;
using System.Xml.Linq;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Practiced_E_commerce.Dto.AllUsers;
using Practiced_E_commerce.Dto.Login;
using Practiced_E_commerce.Dto.Register;
using Practiced_E_commerce.Models;
using Practiced_E_commerce.RepositoryInterface;
using Practiced_E_commerce.Token;

namespace Practiced_E_commerce.Repository
{
    public class RegisterRepository : IRegisterRepoInterface
    {
        private readonly IDbConnection _db;
        private readonly JwtService _jwtservice;
        public RegisterRepository(IDbConnection db , JwtService jwtservice)
        {
            _db = db;
            _jwtservice = jwtservice;
        }

    
        public async Task<RegiterResponceDto> RegisterUser(RegisterDto registerdto)
        {
            //check same Username exists or not
            var checkEmailexists = await _db.ExecuteScalarAsync<int>("Select count(*) From Users Where Email = @useremail" ,
                                      new { useremail  = registerdto.UserEmail });
            if (checkEmailexists != 0)
            {
                throw new Exception("Email Already Exists !");
            }

            //RoleId
            var CustomerRoleId = await _db.ExecuteScalarAsync<int>("Select Id from Roles Where Name = 'Customer'");

            //convert into hashedpassword
            string hashedpassword = BCrypt.Net.BCrypt.HashPassword(registerdto.UserPassword);

            var sql = @"Insert into Users (Name , Email , PasswordHash , RoleId , IsActive , CreatedAt)
                        Output Inserted.* Values (@Name , @Email , @PasswordHash , @RoleId , @IsActive , @CreatedAt)";

            var result = await _db.QuerySingleAsync<Users>(sql, new

            {
                Name = registerdto.UserName,
                Email = registerdto.UserEmail,
                PasswordHash = hashedpassword,
                RoleId = CustomerRoleId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });

            //fetch role name
            var rolename = await _db.ExecuteScalarAsync<string>("Select Name From Roles Where Id = @myid", new { myid = result.RoleId });

            return new RegiterResponceDto
            {
                Id = result.Id,
                Name = result.Name,
                Email = result.Email,
                RoleName = rolename,
                IsActive = result.IsActive,
            };

        }



        public async Task<LoginResponceDto> LoginUser(LoginUserDto logindto)
        {
            //fetched user
            var sql = @"SELECT u.Id , u.Name , u.Email , u.PasswordHash , r.Name AS RoleName
                        FROM Users u
                        INNER JOIN Roles r ON u.RoleId = r.Id
                        WHERE u.Email = @myemail";

            var myuser = await _db.QueryFirstOrDefaultAsync<UserRoleDto>(sql, new { myemail = logindto.UserEmail });
            if (myuser == null || !BCrypt.Net.BCrypt.Verify(logindto.UserPassword, myuser.PasswordHash))
            {
                throw new Exception("Invalid Credentials !");
            }

            //jwt token
            var token = _jwtservice.GenerateToken(myuser);


            return new LoginResponceDto
            {
                Mytoken = token
                
            };

        }

        public async Task<List<AllUsersResponceDto>> AllUsers()
        {
            var sql = "Select u.* , r.Name As RoleName from Users u Inner join Roles r on u.RoleId = r.Id";
            var result = await _db.QueryAsync<AllUsersResponceDto>(sql);
            return result.ToList();
        }
    }
}
