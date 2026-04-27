namespace Practiced_E_commerce.Dto.Products
{
    public class CreateProductResponseDto
    {
        // Product fields
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int StockQuantity { get; set; }
        public string SKU { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }

        // Images (sab ek saath)
        public List<string> ImageUrls { get; set; }
    }
}
