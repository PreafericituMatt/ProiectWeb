using AutoMapper;

using Microsoft.EntityFrameworkCore;
using ProiectWeb.Entities;
using ProiectWebData.Repositories.Interface;
using ProiectWebService.Dtos;
using ProiectWebService.Services.Interfaces;

namespace ProiectWebService.Services.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IMapper _mapper;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IItemRepository _itemsRepository;

        public ShoppingCartService(IMapper mapper, IShoppingCartRepository shoppingCartRepository, IItemRepository itemRepository)
        {
            _mapper = mapper;
            _shoppingCartRepository = shoppingCartRepository;
            _itemsRepository = itemRepository;
        }

        public async Task<ServiceResponse<ShoppingCartItemsDto>> AddItems(int bookId, int quantity, int userId)
        {
            var response = new ServiceResponse<ShoppingCartItemsDto>();

            var valid = _shoppingCartRepository.ValidateBookId(bookId);


            if (valid == false)
            {
                response.Message = "Book not existent";
                response.Success = false;

                return response;
            }

            var finalAmount = CalculateFinalAmount(bookId, quantity);
            var data = await _shoppingCartRepository.AddToCart(bookId, quantity, finalAmount, userId);

            if (data != null)
            {
                response.Data = _mapper.Map<ShoppingCartItems, ShoppingCartItemsDto>(data.Data);
            }
            else
            {
                response.Success = false;
                response.Message = "Add to cart failed";
            }

            return response;
        }

        public async Task<ServiceResponse<List<ShoppingCartDto>>> GetAllItems()
        {
            var response = new ServiceResponse<List<ShoppingCartDto>>();
            var data = await _shoppingCartRepository.GetAllItems();

            if (data != null)
            {
                response.Data = _mapper.Map<List<ShoppingCart>, List<ShoppingCartDto>>(data.Data);
            }
            else
            {
                response.Success = false;
                response.Message = "Shopping cart null";
            }

            return response;
        }

        public async Task<ServiceResponse<List<ShoppingCartItemsDto>>> GetCurrentShoppingCart(int userId)
        {
            var response = new ServiceResponse<List<ShoppingCartItemsDto>>();
            var data = await _shoppingCartRepository.GetCurrentShoppingCart(userId);
            if (data != null)
            {
                response.Data = _mapper.Map<List<ShoppingCartItems>, List<ShoppingCartItemsDto>>(data.Data);
            }
            else
            {
                response.Success = false;
                response.Message = "Shopping cart not found";
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteShoppingCartItem(int shoppingCartId, int userId)
        {
            var response = new ServiceResponse<bool>();
            var data = await _shoppingCartRepository.DeleteShoppingCartItem(shoppingCartId, userId);
            response.Data = data.Data;

            return response;
        }

        public float CalculateFinalAmount(int bookId, int quantity)
        {
            float finalAmount = 0;
            var itemPrice = 0;// _itemRepository.GetItemPrice(bookId);
            finalAmount = itemPrice * quantity;
            return finalAmount;
        }
    }
}
