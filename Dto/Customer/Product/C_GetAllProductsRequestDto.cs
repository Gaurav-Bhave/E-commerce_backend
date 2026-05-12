namespace Practiced_E_commerce.Dto.Customer.Product
{
    public class C_GetAllProductsRequestDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public string? SearchTerm { get; set; }
    }
}
