using System.Data;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Practiced_E_commerce.Execption;
using Practiced_E_commerce.Repository;
using Practiced_E_commerce.Repository.Customer;
using Practiced_E_commerce.RepositoryInterface;
using Practiced_E_commerce.RepositoryInterface.Customer;
using Practiced_E_commerce.Seeder;
using Practiced_E_commerce.Service;
using Practiced_E_commerce.Service.Customer;
using Practiced_E_commerce.ServiceInterface;
using Practiced_E_commerce.ServiceInterface.Customer;
using Practiced_E_commerce.Token;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// 🔥 Activate Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// 🔐 JWT setting
var jwtsetting = builder.Configuration.GetSection("JwtSetting");
var secretkey = jwtsetting.GetValue<string>("SecretKey");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.MapInboundClaims = false;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtsetting.GetValue<string>("Issuer"),
        ValidAudience = jwtsetting.GetValue<string>("Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey)),
        RoleClaimType = ClaimTypes.Role
    };
});

// 🔐 Swagger bearer config
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter JWT Token like: Bearer {your_token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// 🟢 CORS ADD (IMPORTANT)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // React frontend
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Controllers
builder.Services.AddControllers();

// DB Connection
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("dbcs")));

// Seeder
builder.Services.AddScoped<CreateRoleIfNotExists>();

// Services & Repos
builder.Services.AddScoped<IRegisterRepoInterface, RegisterRepository>();
builder.Services.AddScoped<IRegisterServiceInterface, RegisterService>();

builder.Services.AddScoped<IBrandsRepoInterface, BrandsRepository>();
builder.Services.AddScoped<IBrandsServiceInterface, BrandsService>();

builder.Services.AddScoped<ICaterogyRepoInterface, CategoryRepository>();
builder.Services.AddScoped<ICategoryServiceInterface, CategoryService>();

builder.Services.AddScoped<IProductRepoInterface, ProductRepository>();
builder.Services.AddScoped<IProductServiceInterface, ProductService>();

builder.Services.AddScoped<IUsersRepoInterface, UsersRepositrory>();
builder.Services.AddScoped<IUsersServiceInterface, UserService>();

builder.Services.AddScoped<I_CProductRepointerface, C_ProductRepository>();
builder.Services.AddScoped<I_CProductServiceInterface, C_ProductService>();

builder.Services.AddScoped<I_CCartRepositoryInterface, C_CartRepository>();
builder.Services.AddScoped<I_CCartServiceInterface, C_CartService>();

// JWT service
builder.Services.AddScoped<JwtService>();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Seeder run
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<CreateRoleIfNotExists>();
    await seeder.Seed();
}

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global exception
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles();

//// Ya agar custom path hai:
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(
//        Path.Combine(Directory.GetCurrentDirectory(), "images")),
//    RequestPath = "/images"
//});

// 🟢 CORS middleware (VERY IMPORTANT POSITION)
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();