namespace Practiced_E_commerce.Dto.Products
{
    public class Getall_productswithpagination_response_dto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        public List<string> ImageUrl { get; set; } = new List<string>();
        public int TotalCount { get; set; }
        
    }
}
