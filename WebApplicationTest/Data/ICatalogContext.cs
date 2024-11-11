using MongoDB.Driver;
using WebApplicationTest.Models;

namespace WebApplicationTest.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
        IMongoCollection<Order> Orders { get; }
    }
}
