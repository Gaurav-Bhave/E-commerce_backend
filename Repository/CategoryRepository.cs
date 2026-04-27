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
            var sql = "Select * from Categories ORDER BY Id ASC";
            var result = await _db.QueryAsync<Categories>(sql);

            return result.Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }


        public async Task<CreatecategoryrequestDto> CreateCategory(CreatecategoryrequestDto createcategorydto)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Categoryname", createcategorydto.categoryName);

            var result = await _db.ExecuteAsync("CreateCategory", parameter, commandType: CommandType.StoredProcedure);

            return createcategorydto;
        }
    }
}
