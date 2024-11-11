using WebApplicationTest.Models;

namespace WebApplicationTest.Services
{
    public interface IProductService
    {
        Task<Product> AddProduct(Product product);
        Task<List<Product>> GetProductsList();
        Task<Product> GetProductByIdAsync(string id);
    }
}
