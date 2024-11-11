using WebApplicationTest.DataTransferObjects;
using WebApplicationTest.Models;

namespace WebApplicationTest.Services
{
    public interface IOrderService
    {
        Task<Order> AddOrder(OrderRequest request);
        Task<List<OrderResponse>> GetOrdersList();
        Task<Order> GetOrderById(string id);
        Task<double> GetFinalValueOfOrder(string id);
        Task<List<Order>> GetOrderByClientId(int id);
    }
}
