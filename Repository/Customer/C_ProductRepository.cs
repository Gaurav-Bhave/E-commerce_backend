using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Practiced_E_commerce.Dto.Customer.Product;
using Practiced_E_commerce.Dto.Products;
using Practiced_E_commerce.RepositoryInterface.Customer;

namespace Practiced_E_commerce.Repository.Customer
{
    public class C_ProductRepository : I_CProductRepointerface
    {
        private readonly IDbConnection _db;
        public C_ProductRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<PagedProductResponseDto> GetlAllProducts(C_GetAllProductsRequestDto getallproductrequestdto)
        {
            var paramter = new DynamicParameters();

            paramter.Add("@PageNumber", getallproductrequestdto.PageNumber);
            paramter.Add("@PageSize", getallproductrequestdto.PageSize);
            paramter.Add("@SearchTerm", getallproductrequestdto.SearchTerm);

            using var multi = await _db.QueryMultipleAsync(
                "C_GetAllProductsWithPagination",
                paramter,
                commandType: CommandType.StoredProcedure
            );

            // ✅ STEP 1: Total Count
            var totalCount = await multi.ReadFirstAsync<int>();

            // ✅ STEP 2: Products
            var products = (await multi.ReadAsync<C_ProductResponseDto>()).ToList();

            // ✅ STEP 3: Images
            var images = (await multi.ReadAsync<ProductImageResponseDto>()).ToList();

            // ✅ STEP 4: Map images to products
            foreach (var product in products)
            {
                product.Images = images
                    .Where(x => x.ProductId == product.Id)
                    .Select(x => new ProductImageResponseDto
                    {
                        ProductId = x.ProductId,
                        ImageUrl = x.ImageUrl
                    })
                    .ToList();
            }

            return new PagedProductResponseDto
            {
                TotalCount = totalCount,
                Products = products
            };
        }


        //customer side product view details
        public async Task<ProductDetailsResponseDto> C_Productdetailsbyid(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);

            var result = await _db.QueryMultipleAsync("GetProductById", parameter, commandType: CommandType.StoredProcedure);

            //product single row
            var singleproduct = await result.ReadFirstOrDefaultAsync<ProductDetailsResponseDto>();

            if (singleproduct == null)
                return null;

            //mutiple images
            var multipleimages = (await result.ReadAsync<ProductImageDto>()).ToList();



            //convert into main response dto
            singleproduct.Images = multipleimages;

            return singleproduct;
        }
    }
}
