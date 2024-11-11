using MongoDB.Driver;
using WebApplicationTest.Data;
using WebApplicationTest.DataTransferObjects;
using WebApplicationTest.Models;

namespace WebApplicationTest.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICatalogContext _context;
        private readonly IProductService _productService;

        public OrderService(ICatalogContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }
        public async Task<Order> AddOrder(OrderRequest request)
        {
            Order order = new Order()
            {
                ClientId = request.CLientId,
                Products = new()
            };

            foreach(string productId in request.ProductIds)
            {
                Product product = await _productService.GetProductByIdAsync(productId);

                if (product != null) {
                    order.Products.Add(product);
                }
            }
            await _context.Orders.InsertOneAsync(order);
            return order;
        }

        public async Task<List<OrderResponse>> GetOrdersList()
        {
            List<Order> products =  await _context.Orders.Find(_ => true).ToListAsync();

            List<OrderResponse> result = products.Select(x => new OrderResponse()
            {
                Id = x.Id,
                ClientId = x.ClientId,
                Products = x.Products.Select(p => new ProductResponse()
                {
                    Name = p.Name,
                    Price = p.Price,
                    Amount = p.Amount
                }).ToList()
            }).ToList();

            return result;
        }

        public async Task<Order> GetOrderById(string id)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(x => x.Id, id);
            return await _context.Orders.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<double> GetFinalValueOfOrder(string id)
        {
            Order order = await GetOrderById(id);

            if (order == null || order.Products == null)
                return 0;

            return order.Products
                .GroupBy(p => p.Name)
                .Select(x => new {
                    Sum = x.Sum(p => p.Amount * p.Price)
                })
                .Select(x => x.Sum)
                .Sum();

        }

        public async Task<List<Order>> GetOrderByClientId(int id)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(x => x.ClientId, id);
            return await _context.Orders.Find(filter).ToListAsync();
        }

    }
}
