namespace Practiced_E_commerce.Dto.Customer.Product
{
    public class C_ProductResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }
        public bool IsDeleted { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public List<ProductImageResponseDto> Images { get; set; } = new();
    }
}
