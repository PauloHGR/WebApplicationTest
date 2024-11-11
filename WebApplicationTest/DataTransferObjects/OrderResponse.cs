namespace WebApplicationTest.DataTransferObjects
{
    public class OrderResponse()
    {
        public string? Id { get; set; }
        public int ClientId { get; set; }
        public List<ProductResponse>? Products { get; set; }
    }

    public class ProductResponse()
    {
        public string? Name { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
    }
}
