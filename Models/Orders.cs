namespace Practiced_E_commerce.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
