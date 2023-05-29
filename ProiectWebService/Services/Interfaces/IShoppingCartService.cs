using ProiectWebService.Dtos;

namespace ProiectWebService.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ServiceResponse<List<ShoppingCartDto>>> GetAllItems();
        Task<ServiceResponse<ShoppingCartItemsDto>> AddItems(int bookId,int quantity, int userId);
        Task<ServiceResponse<List<ShoppingCartItemsDto>>> GetCurrentShoppingCart( int userId);
        Task<ServiceResponse<bool>> DeleteShoppingCartItem(int shoppingCartId, int userId);
        public float CalculateFinalAmount(int bookId, int quantity);
    }
}
