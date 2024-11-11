using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApplicationTest.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public int ClientId { get; set; }
        public List<Product>? Products { get; set; }
    }
}
