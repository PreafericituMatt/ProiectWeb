
using ProiectWeb.Entities;

namespace ProiectWebData.Repositories.Interface
{
    public interface IShoppingCartRepository
    {
        Task<ServiceResponse<List<ShoppingCart>>> GetAllItems();
        Task<ServiceResponse<ShoppingCartItems>> AddToCart(int bookId,int quantity,float finalAmount, int userId);
        Task<ServiceResponse<List<ShoppingCartItems>>> GetCurrentShoppingCart( int id);
        Task<ServiceResponse<bool>> DeleteShoppingCartItem(int shoppingCartId, int userId);
        bool ValidateBookId(int id);
    }
}
