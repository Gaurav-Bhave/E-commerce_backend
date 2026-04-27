namespace Practiced_E_commerce.Dto.Products
{
    public class CreateProductRequestDto
    {
        public string ProductName { get; set; }     
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int StockQuantity { get; set; }
        public List<IFormFile> ProductImages { get; set; }
    }
}
