using Practiced_E_commerce.Dto.Category;

namespace Practiced_E_commerce.RepositoryInterface
{
    public interface ICaterogyRepoInterface
    {
        Task<List<CategoryDto>> GetAllCatergory();
        Task<CreatecategoryrequestDto> CreateCategory(CreatecategoryrequestDto createcategorydto);
    }
}
