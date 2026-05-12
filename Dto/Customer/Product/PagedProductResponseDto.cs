namespace Practiced_E_commerce.Dto.Customer.Product
{
    public class PagedProductResponseDto
    {
        public int TotalCount { get; set; }

        public List<C_ProductResponseDto> Products { get; set; } = new();
    }
}
