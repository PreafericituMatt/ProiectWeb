using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProiectWeb.Entities;
using ProiectWebData.Entities;
using ProiectWebService.Dtos;

namespace ProiectWebService
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Items, ItemsDto>();
            CreateMap<ItemsDto, Items>();

            CreateMap<ServiceResponse<string>, ProiectWebData.ServiceResponse<string>>();
            CreateMap<ProiectWebData.ServiceResponse<string>, ServiceResponse<string>>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<User, AuthDto>();
            CreateMap<AuthDto, User>();

            CreateMap<User, AuthResponseDto>();
            CreateMap<AuthResponseDto, User>();

            CreateMap<ShoppingCart, ShoppingCartDto>();
            CreateMap<ShoppingCartDto, ShoppingCart>();

            CreateMap<ShoppingCartItemsDto, ShoppingCartItems>();
            CreateMap<ShoppingCartItems, ShoppingCartItemsDto>();

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();

            CreateMap<OrderDetails, OrderDetailsDto>();
            CreateMap<OrderDetailsDto, OrderDetails>();

            CreateMap<OrderItems, OrderItemsDto>();
            CreateMap<OrderItemsDto, OrderDetailsDto>();

            CreateMap<GetOrderDto, Order>();
            CreateMap<Order, GetOrderDto>();

            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>();

        }
    }
}
