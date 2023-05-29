


using ProiectWeb.Entities;
using ProiectWebData;

namespace IProiectWebData.Repositories.Interface
{
    public interface IOrderRepository
    {
        Task<ServiceResponse<Order>> PlaceOrder(Order order, int userId);
        Task<ServiceResponse<List<Order>>> GetUserOrders(int userId);
        Task<ServiceResponse<Order>> UpdateOrder(Order updatedOrder, int userId, int orderId);
    }
}
