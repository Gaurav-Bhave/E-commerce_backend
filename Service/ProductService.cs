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


        public async Task<ResponceModel> GetProducts(Getall_productwithpagination_request_Dto getalldto)
        {
            var result = await _productrepo.GetProducts(getalldto);

            return new ResponceModel
            { 
                StatusCode = 200,
                Message = "Product fetch successfully !" ,
                Data = result
            };
        }

        public async Task<ResponceModel> Getproductbyid(int id)
        {
            var result = await _productrepo.Getproductbyid(id);

            return new ResponceModel
            {
                StatusCode =200,
                Message = "Single product successfully !",
                Data = result
            };
        }

        public async Task<ResponceModel> Deleteproductbyid(int id)
        {
            var result = await _productrepo.Deleteproductbyid(id);

            return new ResponceModel
            { 
                StatusCode=200,
                Message = "product successfully deleted !",
                Data  = result
            };
        }

        public async Task<ResponceModel> Updateproduct(UpdateProductRequestDto updateproductdto)
        {
            var result = await _productrepo.Updateproduct(updateproductdto);

            return new ResponceModel
            {
                StatusCode = 200,
                Message = "product update sucessfully !",
                Data = result
            };
        }


        //summery for dashboard
        public async Task<ResponceModel> Summery()
        {
            var result = await _productrepo.Summery();

            return new ResponceModel
            {
                StatusCode = 200,
                Message = "Fetched sucessfully Dashboard summery !",
                Data= result
            };
        }
    }
}
