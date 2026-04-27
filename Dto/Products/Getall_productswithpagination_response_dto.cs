namespace Practiced_E_commerce.Dto.Products
{
    public class Getall_productswithpagination_response_dto
    {
        public int Productid { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ProductPrice { get; set; }
        public int Productcategoryid { get; set; }
        public string Productcategoryname { get; set; }
        public int Productbrandid { get; set; }
        public string Productbrandname { get; set; }
        public int Productstokequantity { get; set; }
        public int Productstokekeepingunit { get; set; }
        public bool Productisdeleted { get; set; }
        public DateTime ProductCreatedAt { get; set; }
        public List<string> ProdctImages { get; set; }
    }
}
