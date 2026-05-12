namespace Practiced_E_commerce.Dto.Customer.Cart
{
    public class UpdateCartQuantityRequestDto
    {

        public int ProductId { get; set; }
        public string Action { get; set; }  // INCREMENT / DECREMENT
    }
}
