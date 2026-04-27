using Practiced_E_commerce.Dto.Products;
using Practiced_E_commerce.Models;
using Practiced_E_commerce.RepositoryInterface;
using Practiced_E_commerce.ServiceInterface;

namespace Practiced_E_commerce.Service
{
    public class ProductService : IProductServiceInterface
    {
        private readonly IProductRepoInterface _productrepo;
        public ProductService(IProductRepoInterface productrepo)
        {
            _productrepo = productrepo;
        }

        public async Task<ResponceModel> Createproduct(CreateProductRequestDto createproductdto)
        {
            var result = await _productrepo.Createproduct(createproductdto);

            return new ResponceModel
            {
                StatusCode = 200,
                Message = "Product created successfully !",
                Data = result
            };
        }
    }
}
