using System.Data;
using System.Xml.Linq;
using Dapper;
using Practiced_E_commerce.Dto.Products;
using Practiced_E_commerce.Models;
using Practiced_E_commerce.RepositoryInterface;

namespace Practiced_E_commerce.Repository
{
    public class ProductRepository : IProductRepoInterface
    {
        private readonly IDbConnection _db;
        public ProductRepository(IDbConnection db)
        {
            _db = db;
        }


        public async Task<List<ProductListResponceDto>> GetAllProduct()
        {
            var sql = @"select p.* , b.Name as BrandName, c.Name as CategoryName from Products p 
                        inner join 
                        Categories c
                        on p.CategoryId = c.Id 
                        inner join
                        Brands b 
                        on p.BrandId = b.Id ";

            var result = await _db.QueryAsync<ProductListResponceDto>(sql);

            return result.ToList();
        }


        public async Task<ProductCreateDto> CreateProduct(ProductCreateDto productcreatedto)
        {
            var sql = @"Insert into Products (Name , Description , Price , CategoryId , BrandId , StockQuantity , SKU) 
                        output inserted.* values (@name , @description , @price , @categoryid , @brandid , @StokeQuantity , @sku)";

            var result = await _db.QueryFirstAsync<Products>(sql,
                            new
                            {
                                name = productcreatedto.Name,
                                description = productcreatedto.Description,
                                price = productcreatedto.Price,
                                categoryid = productcreatedto.CategoryId,
                                brandid = productcreatedto.BrandId,
                                StokeQuantity = productcreatedto.StockQuantity,
                                sku = productcreatedto.SKU,
                            });

            return new ProductCreateDto
            {
                Name = result.Name,
                Description = result.Description,
                Price = result.Price,
                CategoryId = result.CategoryId,
                BrandId = result.BrandId,
                StockQuantity = result.StockQuantity,
                SKU = result.SKU
            };

        }
    }
}
