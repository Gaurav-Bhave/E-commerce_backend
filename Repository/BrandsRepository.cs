using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Practiced_E_commerce.Dto.Brands;
using Practiced_E_commerce.Models;
using Practiced_E_commerce.RepositoryInterface;

namespace Practiced_E_commerce.Repository
{
    public class BrandsRepository : IBrandsRepoInterface
    {
        private readonly IDbConnection _db;
        public BrandsRepository(IDbConnection db)
        {
            _db = db;
        }

       

        public async Task<List<BrandsDto>> GetAllBrands()
        {
            var sql = "Select * from Brands ORDER BY Id ASC";
            var result = await _db.QueryAsync<Brands>(sql);

            return result.Select(x => new BrandsDto
            { 
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }



        public async Task<Createbrandrequest> CreateBrand(Createbrandrequest mycreatebrandrequrt)
        {
            var parameter = new DynamicParameters();

            parameter.Add("@BrandName", mycreatebrandrequrt.brandName);

            var result = await _db.ExecuteAsync("CreateBrands", parameter, commandType: CommandType.StoredProcedure);

            return mycreatebrandrequrt;
        }
    }
}
