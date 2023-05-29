using AutoMapper;
using IProiectWebData.Repositories.Interface;
using ProiectWeb.Entities;
using ProiectWebService.Dtos;
using ProiectWebService.Services.Interfaces;
using System.ComponentModel;
using System.Diagnostics;

namespace ProiectWebService.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<ServiceResponse<List<GetOrderDto>>> GetUserOrders(int userId)
        {
            var response = new ServiceResponse<List<GetOrderDto>>();
            var data = await _orderRepository.GetUserOrders(userId);

            if (data != null)
            {
                response.Data = _mapper.Map<List<Order>, List<GetOrderDto>>(data.Data);
            }
            else
            {
                response.Success = false;
                response.Message = "Orders null";
            }

            return response;
        }

        public async Task<ServiceResponse<OrderDto>> PlaceOrder(OrderDto orderItems, int userId)
        {
            var response = new ServiceResponse<OrderDto>();        

            if (orderItems.OrderDetails.BillingAddress.Country == null || orderItems.OrderDetails.BillingAddress.PhoneNumber == null || orderItems.OrderDetails.BillingAddress.CustomerAddress == null)
            {
                response.Success = false;
                response.Message = "Delivery information required";

                return response;
            }
           
            orderItems.OrderDetails.Observations = orderItems.OrderDetails.Observations ?? string.Empty;

            if (orderItems.OrderDetails.DeliveryAddress.Country == null || orderItems.OrderDetails.DeliveryAddress.PhoneNumber == null || orderItems.OrderDetails.DeliveryAddress.CustomerAddress == null || orderItems.OrderDetails.UseBillingAddrForDelivery == true)
            {
                orderItems.OrderDetails.DeliveryAddress.Country = orderItems.OrderDetails.BillingAddress.Country;
                orderItems.OrderDetails.DeliveryAddress.PhoneNumber = orderItems.OrderDetails.BillingAddress.PhoneNumber;
                orderItems.OrderDetails.DeliveryAddress.CustomerAddress = orderItems.OrderDetails.BillingAddress.CustomerAddress;
            }

            var order = _mapper.Map<OrderDto, Order>(orderItems);

            if (order == null)
            {
                response.Success = false;
                response.Message = "Order null";

                return response;
            }

            var data = await _orderRepository.PlaceOrder(order, userId);

            if (data.Success == false)
            {
                response.Success = false;
                response.Message = "Order null";

                return response;
            }
            else
            {
                response.Message = "Your order has been placed";
                response.Data = _mapper.Map<Order, OrderDto>(data.Data);

                return response;
            }
        }

        public async Task<ServiceResponse<OrderDto>> UpdateOrder(OrderDto updatedOrder, int userId, int orderId)
        {
            var response = new ServiceResponse<OrderDto>();
            
            if (updatedOrder == null)
            {
                response.Success = false;
                response.Message = "Updated order is null";

                return response;
            }

            if (updatedOrder.OrderDetails.Observations == null)
            {
                updatedOrder.OrderDetails.Observations = string.Empty;
            }

            var order = _mapper.Map<OrderDto, Order>(updatedOrder);
            var data = await _orderRepository.UpdateOrder(order, userId, orderId);

            if (data.Success == false)
            {
                response.Success = false;
                response.Message = "Modify failed";

                return response;
            }
            else
            {
                response.Data = _mapper.Map<Order, OrderDto>(data.Data);
            }

            return response;
        }      
    }
}
