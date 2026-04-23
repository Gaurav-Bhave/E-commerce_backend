using System.Data;
using System.Xml.Linq;
using BCrypt.Net;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Practiced_E_commerce.Models;

namespace Practiced_E_commerce.Seeder
{
    public class CreateRoleIfNotExists
    {
        private readonly IDbConnection _db;
        public CreateRoleIfNotExists(IDbConnection db)
        {
            _db = db;
        }

        public async Task Seed()
        {
            //Default Admin Role create
            var adminroleexists = await _db.ExecuteScalarAsync<int>("Select count(*) from Roles where Name = 'Admin'");
            if (adminroleexists == 0)
            {
                await _db.ExecuteAsync("Insert into roles (Name) values ('Admin')");
            }

            //Default Customer Role create
            var customerroleexists = await _db.ExecuteScalarAsync<int>("Select count(*) from Roles where Name = 'Customer'");
            if (customerroleexists == 0)
            {
                await _db.ExecuteAsync("Insert into roles (Name) values ('Customer')");
            }


            //Check Default AdminUser 
            var adminuserexists = await _db.ExecuteScalarAsync<int>("Select Count(*) from Users Where Email = 'admin@gmail.com'");
            if (adminuserexists == 0)
            {
                //finding admin role id
                var adminroleid = await _db.ExecuteScalarAsync<int>("Select Id from Roles where Name = 'Admin'");

                //Converting Hashing Password
                string plaintextpassword = "Admin@123";
                string PasswordHash = BCrypt.Net.BCrypt.HashPassword(plaintextpassword);

                //Creating Defalut AdminUser
                await _db.ExecuteAsync(@"Insert into Users (Name , Email , PasswordHash , RoleId , IsActive , CreatedAt)
                                        Values (@Name , @Email , @PasswordHash , @RoleId , @IsActive , @CreatedAt)",
                    new
                    {
                        Name = "Admin123",
                        Email = "admin@gmail.com",
                        PasswordHash = PasswordHash ,
                        RoleId = adminroleid ,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    });
                    
            }
        }
    }
}


