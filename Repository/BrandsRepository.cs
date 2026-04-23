using System.Data;
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
            var sql = "Select * from Brands";
            var result = await _db.QueryAsync<Brands>(sql);

            return result.Select(x => new BrandsDto
            { 
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }
    }
}
