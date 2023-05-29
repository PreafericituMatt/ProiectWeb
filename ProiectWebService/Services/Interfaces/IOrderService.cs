
using ProiectWebService.Dtos;

namespace ProiectWebService.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ServiceResponse<OrderDto>> PlaceOrder(OrderDto orderItemsDto, int userId);       
        Task<ServiceResponse<List<GetOrderDto>>> GetUserOrders(int userId);
        Task<ServiceResponse<OrderDto>> UpdateOrder(OrderDto updatedOrder, int userId, int orderId);
    }
}
