using Practiced_E_commerce.Dto.Category;
using Practiced_E_commerce.Models;
using Practiced_E_commerce.RepositoryInterface;
using Practiced_E_commerce.ServiceInterface;

namespace Practiced_E_commerce.Service
{
    public class CategoryService : ICategoryServiceInterface
    {
        private readonly ICaterogyRepoInterface _categoryrepo;
        public CategoryService(ICaterogyRepoInterface categoryrepo)
        {
            _categoryrepo = categoryrepo;
        }

      
        public async Task<ResponceModel> GetAllCategory()
        {
            var result = await _categoryrepo.GetAllCatergory();

            return new ResponceModel
            {
                StatusCode = 200,
                Message = "List of categories",
                Data = result
            };
        }


        public async Task<ResponceModel> CreateCategory(CreatecategoryrequestDto createcategorydto)
        {
            var result = await _categoryrepo.CreateCategory(createcategorydto);

            return new ResponceModel
            {
                StatusCode= 200,
                Message = "Create category successfully !",
                Data= result
            };
        }

    }
}
