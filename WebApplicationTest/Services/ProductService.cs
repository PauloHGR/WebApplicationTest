using MongoDB.Bson;
using MongoDB.Driver;
using WebApplicationTest.Data;
using WebApplicationTest.Models;

namespace WebApplicationTest.Services
{
    public class ProductService : IProductService
    {
        private readonly ICatalogContext _context;

        public ProductService(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<List<Product>> GetProductsList()
        {
            return await _context.Products.Find(_ => true).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, id);
            return await _context.Products.Find(filter).FirstOrDefaultAsync();
        }
    }
}
