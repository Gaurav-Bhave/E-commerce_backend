namespace Practiced_E_commerce.Dto.Products
{
    public class Getall_productwithpagination_request_Dto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 3;
        public string? Search { get; set; } = null;
        public int? PriceRange { get; set; } = null;
    }
}
