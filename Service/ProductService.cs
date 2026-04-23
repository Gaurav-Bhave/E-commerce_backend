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


        public async Task<ResponceModel> GetAllProducts()
        {
            var result = await _productrepo.GetAllProduct();

            return new ResponceModel
            {
                StatusCode = 200,
                Message = "All Product List",
                Data = result
            };
        }



        public async Task<ResponceModel> CreateProduct(ProductCreateDto productcreatedto)
        {
            var result = await _productrepo.CreateProduct(productcreatedto);

            return new ResponceModel
            {
                StatusCode = 200,
                Message = "Product Created Successfully !",
                Data = result
            };
        }
    }
}
