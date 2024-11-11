using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApplicationTest.Models;

namespace WebApplicationTest.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }
        public IMongoCollection<Order> Orders { get; }

        public CatalogContext(IOptions<ItemCatalog> itemsDB)
        {
            var mongoClient = new MongoClient(itemsDB.Value.ConnMONGODB);
            var mongoDatabase = mongoClient.GetDatabase(itemsDB.Value.DatabaseName);
            Products = mongoDatabase.GetCollection<Product>(itemsDB.Value.ProductCollectionName);
            Orders = mongoDatabase.GetCollection<Order>(itemsDB.Value.OrderCollectionName);
        }
        
    }
}
