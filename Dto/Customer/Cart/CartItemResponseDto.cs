namespace Practiced_E_commerce.Dto.Customer.Cart
{
    public class CartItemResponseDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public decimal TotalPrice { get; set; }
        public string ImageUrl { get; set; }
    }
}
