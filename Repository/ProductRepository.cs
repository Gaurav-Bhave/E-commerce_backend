using System.Data;
using System.Text.Json.Serialization;
using Dapper;
using Practiced_E_commerce.Dto.Products;
using Practiced_E_commerce.RepositoryInterface;
using Newtonsoft.Json;
using Practiced_E_commerce.Dto.Summery;


namespace Practiced_E_commerce.Repository
{
    public class ProductRepository : IProductRepoInterface
    {
        private readonly IDbConnection _db;

        public ProductRepository(IDbConnection db)
        {
            _db = db;
        }





        //create product
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


        //get all products

        public async Task<List<Getall_productswithpagination_response_dto>> GetProducts(Getall_productwithpagination_request_Dto getalldto)
        {
            try
            {

                var parameters = new DynamicParameters();

                parameters.Add("@Page", getalldto.Page);
                parameters.Add("@PageSize", getalldto.PageSize);
                parameters.Add("@Search", getalldto.Search);
                parameters.Add("@PriceRange", getalldto.PriceRange);

                var mydata = await _db.QueryMultipleAsync("Getallproducts_with_pagination_searching_filtering_new", parameters, commandType: CommandType.StoredProcedure);

                //total count
                int totalcount = await mydata.ReadFirstOrDefaultAsync<int>();

                //products
                var rawProducts = (await mydata.ReadAsync<Getall_productswithpagination_response_dto>()).ToList();

                //images
                var allimages = (await mydata.ReadAsync<dynamic>()).ToList();

                var products = rawProducts.Select(p => new Getall_productswithpagination_response_dto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    IsDeleted = p.IsDeleted,
                    CreatedAt = p.CreatedAt,
                    CategoryName = p.CategoryName,
                    BrandName = p.BrandName,
                    TotalCount = totalcount,

                    ImageUrl = allimages.Where(img => img.ProductId == p.Id)
                           .Select(img => (string)img.ImageUrl)
                           .ToList()

                }).ToList();

                return products;

            }
            catch (Exception ex)
            {
                throw new Exception("Get products failed: " + ex.Message);
            }
        }


        //get prodct by id
        public async Task<ProductDetailsResponseDto> Getproductbyid(int id)
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


        //soft delete product
        public async Task<DeleteProductResponse_Dto> Deleteproductbyid(int id)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@ProductId", id);

                var result = await _db.ExecuteAsync(
                    "SoftDeleteProduct",
                    parameter,
                    commandType: CommandType.StoredProcedure
                );


                return new DeleteProductResponse_Dto
                {
                    Message = "Product deleted successfully!"
                };


            }
            catch (Exception ex)
            {
                return new DeleteProductResponse_Dto
                {
                    Message = "Error while deleting: " + ex.Message
                };
            }
        }


        //update product with multiple images 
        public async Task<UpdateProductResponseDto> Updateproduct(UpdateProductRequestDto requestDto)
        {
            try
            {
                // Parse deleted ids safely
                List<int> deletedIds = new List<int>();
                if (!string.IsNullOrWhiteSpace(requestDto.DeletedImageIds))
                {
                    deletedIds = requestDto.DeletedImageIds
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => {
                            if (int.TryParse(s.Trim(), out var v)) return v;
                            return 0;
                        })
                        .Where(i => i > 0)
                        .Distinct()
                        .ToList();
                }

                // Ensure connection open
                if (_db.State != ConnectionState.Open)
                    _db.Open();

                using (var tran = _db.BeginTransaction())
                {
                    try
                    {
                        // 1) Fetch file urls for deleted ids BEFORE deletion so we can remove files from disk
                        IEnumerable<string> filesToDelete = Enumerable.Empty<string>();
                        if (deletedIds.Any())
                        {
                            filesToDelete = await _db.QueryAsync<string>(
                                "SELECT ImageUrl FROM ProductImages WHERE Id IN @Ids",
                                new { Ids = deletedIds },
                                transaction: tran
                            );
                        }

                        // 2) Call stored procedure to update product and delete DB rows
                        var param = new DynamicParameters();
                        param.Add("@productid", requestDto.ProductId, DbType.Int32);
                        param.Add("@productname", requestDto.ProductName, DbType.String);
                        param.Add("@description", requestDto.Description, DbType.String);
                        param.Add("@price", requestDto.Price, DbType.Decimal);
                        param.Add("@categoryid", requestDto.CategoryId, DbType.Int32);
                        param.Add("@brandid", requestDto.BrandId, DbType.Int32);
                        param.Add("@stockquantity", requestDto.StockQuantity, DbType.Int32);
                        param.Add("@deletedImageIds", deletedIds.Any() ? string.Join(",", deletedIds) : null, DbType.String);

                        await _db.ExecuteAsync(
                            "sp_product_update_core_v1",
                            param,
                            commandType: CommandType.StoredProcedure,
                            transaction: tran
                        );

                        // 3) Delete files from disk (safe checks). Do not fail the whole transaction on file delete errors.
                        if (filesToDelete != null && filesToDelete.Any())
                        {
                            var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                            foreach (var url in filesToDelete)
                            {
                                try
                                {
                                    if (string.IsNullOrWhiteSpace(url)) continue;

                                    // skip absolute urls
                                    if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                                        url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                                    {
                                        continue;
                                    }

                                    // extract filename and build path
                                    var fileName = Path.GetFileName(url);
                                    if (string.IsNullOrWhiteSpace(fileName)) continue;

                                    var fullPath = Path.Combine(imagesFolder, fileName);
                                    if (File.Exists(fullPath))
                                    {
                                        File.Delete(fullPath);
                                    }
                                }
                                catch
                                {
                                    // swallow file deletion errors (optionally log)
                                }
                            }
                        }

                        // 4) Insert new images (if any)
                        if (requestDto.NewImages != null && requestDto.NewImages.Any())
                        {
                            var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                            if (!Directory.Exists(folder))
                                Directory.CreateDirectory(folder);

                            foreach (var file in requestDto.NewImages)
                            {
                                var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
                                var path = Path.Combine(folder, fileName);

                                using (var stream = new FileStream(path, FileMode.Create))
                                {
                                    await file.CopyToAsync(stream);
                                }

                                await _db.ExecuteAsync(
                                    "INSERT INTO ProductImages(ProductId, ImageUrl) VALUES(@pid,@url)",
                                    new { pid = requestDto.ProductId, url = "/images/" + fileName },
                                    transaction: tran
                                );
                            }
                        }

                        tran.Commit();

                        return new UpdateProductResponseDto
                        {
                            ProductId = requestDto.ProductId,
                            Message = "Updated Successfully"
                        };
                    }
                    catch
                    {
                        try { tran.Rollback(); } catch { /* ignore */ }
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Update product failed: " + ex.Message);
            }
        }





        ////update product with multiple images
        //public async Task<UpdateProductResponseDto> Updateproduct(UpdateProductRequestDto updateproductdto)
        //{
        //    try
        //    {
        //        var param = new DynamicParameters();

        //        // ✅ product fields
        //        param.Add("@productid", updateproductdto.ProductId);
        //        param.Add("@productname", updateproductdto.ProductName);
        //        param.Add("@description", updateproductdto.Description);
        //        param.Add("@price", updateproductdto.Price);
        //        param.Add("@categoryid", updateproductdto.CategoryId);
        //        param.Add("@brandid", updateproductdto.BrandId);
        //        param.Add("@stockquantity", updateproductdto.StockQuantity);


        //        // ✅ images list (TVP)
        //        var table = new DataTable();
        //        table.Columns.Add("Id", typeof(int));
        //        table.Columns.Add("ImageUrl", typeof(string));

        //        foreach (var img in updateproductdto.Images)
        //        {
        //            table.Rows.Add(img.Id, img.ImageUrl);
        //        }

        //        param.Add("@images", table.AsTableValuedParameter("ProductImageTypeedit"));


        //        // ✅ SP call
        //        await _db.ExecuteAsync(
        //            "sp_updateproductwithimages",
        //            param,
        //            commandType: CommandType.StoredProcedure
        //        );


        //        // ✅ response
        //        return new UpdateProductResponseDto
        //        {
        //            ProductId = updateproductdto.ProductId,
        //            Message = "Product updated successfully"
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Update failed: " + ex.Message);
        //    }
        //}



        //update product with multiple images
        //public async Task<UpdateProductResponseDto> Updateproduct(UpdateProductRequestDto requestDto)
        //{
        //    try
        //    {
        //        // 🔥 DELETE IDS SAFE PARSE
        //        var deletedIds = new List<int>();

        //        if (!string.IsNullOrWhiteSpace(requestDto.DeletedImageIds))
        //        {
        //            deletedIds = requestDto.DeletedImageIds
        //                .Split(',', StringSplitOptions.RemoveEmptyEntries)
        //                .Select(x => int.TryParse(x, out var v) ? v : 0)
        //                .Where(v => v > 0)
        //                .ToList();
        //        }

        //        // 🔥 SAVE NEW IMAGES ONLY
        //        var table = new DataTable();
        //        table.Columns.Add("Id", typeof(int));
        //        table.Columns.Add("ImageUrl", typeof(string));

        //        if (requestDto.NewImages != null)
        //        {
        //            foreach (var file in requestDto.NewImages)
        //            {
        //                string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

        //                string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

        //                if (!Directory.Exists(folder))
        //                    Directory.CreateDirectory(folder);

        //                string path = Path.Combine(folder, fileName);

        //                using (var stream = new FileStream(path, FileMode.Create))
        //                {
        //                    await file.CopyToAsync(stream);
        //                }

        //                table.Rows.Add(0, "/images/" + fileName);
        //            }
        //        }

        //        var param = new DynamicParameters();

        //        param.Add("@productid", requestDto.ProductId);
        //        param.Add("@productname", requestDto.ProductName);
        //        param.Add("@description", requestDto.Description);
        //        param.Add("@price", requestDto.Price);
        //        param.Add("@categoryid", requestDto.CategoryId);
        //        param.Add("@brandid", requestDto.BrandId);
        //        param.Add("@stockquantity", requestDto.StockQuantity);

        //        param.Add("@images", table.AsTableValuedParameter("ProductImageTypeedit"));

        //        param.Add("@deletedImageIds",
        //            deletedIds.Count > 0 ? string.Join(",", deletedIds) : null);

        //        var deletedPaths = await _db.QueryAsync<string>(
        //            "sp_updateproductwithimages",
        //            param,
        //            commandType: CommandType.StoredProcedure
        //        );

        //        // delete physical files
        //        foreach (var path in deletedPaths)
        //        {
        //            var full = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path.TrimStart('/'));
        //            if (File.Exists(full))
        //                File.Delete(full);
        //        }

        //        return new UpdateProductResponseDto
        //        {
        //            ProductId = requestDto.ProductId,
        //            Message = "Updated Successfully"
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}





        //overall summery
        public async Task<Overall_Data_Response> Summery()
        {
            var multi = await _db.QueryMultipleAsync("sp_totaluser_order_totalrevenue", commandType: CommandType.StoredProcedure);

            var totaluser = await multi.ReadFirstAsync<int>();
            var totalorder = await multi.ReadFirstAsync<int>();
            var totalrevenue = await multi.ReadFirstAsync<int>();

            return new Overall_Data_Response
            {
                totaluser = totaluser,
                totalorders = totalorder,
                totalrevenue = totalrevenue
            };
        }









    }
}

