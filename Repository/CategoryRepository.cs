using System.Data;
using Dapper;
using Practiced_E_commerce.Dto.Category;
using Practiced_E_commerce.Models;
using Practiced_E_commerce.RepositoryInterface;

namespace Practiced_E_commerce.Repository
{
    public class CategoryRepository : ICaterogyRepoInterface
    {
        private readonly IDbConnection _db;
        public CategoryRepository(IDbConnection db)
        {
            _db = db;
        }


        public async Task<List<CategoryDto>> GetAllCatergory()
        {
            var sql = "Select * from Categories";
            var result = await _db.QueryAsync<Categories>(sql);

            return result.Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }
    }
}
