using System.Data;
using Dapper;
using Practiced_E_commerce.Dto.Products;
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

        public async Task<CreateProductResponseDto> Createproduct(CreateProductRequestDto createproductdto)
        {
            try
            {
                var param = new DynamicParameters();
                string sku = Generatesku(createproductdto.ProductName);

                param.Add("@Name", createproductdto.ProductName);
                param.Add("@Description", createproductdto.Description);
                param.Add("@Price", createproductdto.Price);
                param.Add("@CategoryId", createproductdto.CategoryId);
                param.Add("@BrandId", createproductdto.BrandId);
                param.Add("@StockQuantity", createproductdto.StockQuantity);
                param.Add("@Sku", sku);



                // ✅ Step 1: IFormFile save karo aur path lo
                var imageUrls = new List<string>();

                foreach (var img in createproductdto.ProductImages)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                    string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                    // ✅ Folder nahi hai toh banao
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string filePath = Path.Combine(folderPath, fileName);

                    // ✅ File disk pe save karo
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await img.CopyToAsync(stream);
                    }

                    imageUrls.Add("/images/" + fileName);
                }

                // ✅ Step 2: TVP mein string paths daalo
                var table = new DataTable();
                table.Columns.Add("ImageUrl", typeof(string));

                foreach (var url in imageUrls)
                {
                    table.Rows.Add(url);
                }

                param.Add("@Images", table.AsTableValuedParameter("ProductImageType"));

                // ✅ Step 3: SP call karo
                int productId = await _db.QuerySingleAsync<int>(
                    "sp_Createproductswithimages",
                    param,
                    commandType: CommandType.StoredProcedure
                );

                return new CreateProductResponseDto
                {
                    ProductId = productId,
                    Name = createproductdto.ProductName,
                    Description = createproductdto.Description,
                    Price = createproductdto.Price,
                    CategoryId = createproductdto.CategoryId,
                    BrandId = createproductdto.BrandId,
                    StockQuantity = createproductdto.StockQuantity,
                    SKU = sku,
                    ImageUrls = imageUrls  
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Create product failed: " + ex.Message);
            }
        }

        private string Generatesku(string name)
        {
            var random = new Random();
            int number = random.Next(1000, 9999);

            string part = name.Length >= 3
                ? name.Substring(0, 3).ToUpper()
                : name.ToUpper();

            return $"{part}-{number}";
        }



        //getall products with pagination
        public async Task<Getall_productswithpagination_response_dto> GetAllProducts(Getall_productwithpagination_request_Dto getallproductdto)
        {
            var paramter = new DynamicParameters();

            paramter.Add("@Page", getallproductdto.page);
            paramter.Add("@PageSize", getallproductdto.pagesize);

            
        }
    }
}